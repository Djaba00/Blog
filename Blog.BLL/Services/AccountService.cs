using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Blog.WebService.Externtions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Blog.BLL.Services
{
    public class AccountService : IAccountService
    {
        readonly ILogger<AccountService> logger;
        readonly IUnitOfWork db;
        readonly IMapper mapper;

        public AccountService(ILogger<AccountService> logger, IUnitOfWork db, IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<UserAccountModel>> GetAllAcсountsAsync()
        {
            var accounts = await db.UserAccounts.GetAllAsync();

            var result = new List<UserAccountModel>();

            foreach (var account in accounts)
            {
                var tempAcc = mapper.Map<UserAccountModel>(account);

                var accRoles = await db.UserAccounts.GetUserRolesAsync(account);

                tempAcc.Roles = new List<AccountRoleModel>();

                foreach(var role in accRoles)
                {
                    tempAcc.Roles.Add(new AccountRoleModel() { Name = role });
                }

                result.Add(tempAcc);
            }

            logger.LogInformation("Accounts list has been received");

            return result;
        }

        public async Task<List<UserAccountModel>> GetAccountsByRoleAsync(string roleName)
        {
            var accounts = await db.UserAccounts.GetAccountsByRoleAsync(roleName);

            var result = new List<UserAccountModel>();

            foreach (var account in accounts)
            {
                var tempAcc = mapper.Map<UserAccountModel>(account);

                var accRoles = await db.UserAccounts.GetUserRolesAsync(account);

                tempAcc.Roles = new List<AccountRoleModel>();

                foreach (var role in accRoles)
                {
                    tempAcc.Roles.Add(new AccountRoleModel() { Name = role });
                }

                result.Add(tempAcc);
            }

            return result;
        }

        public async Task<UserAccountModel> GetAccountByIdAsync(string id)
        {
            var user = await db.UserAccounts.GetByIdAsync(id);

            var result = mapper.Map<UserAccountModel>(user);

            var userRoles = await db.UserAccounts.GetUserRolesAsync(user);

            var allRoles = await db.RoleManager.Roles.ToListAsync();

            result.Roles = new List<AccountRoleModel>();

            foreach (var role in allRoles)
            {
                var userRole = mapper.Map<AccountRoleModel>(role);

                result.Roles.Add(userRole);
            }

            foreach (var role in userRoles)
            {
                result.Roles.FirstOrDefault(r => r.Name == role).Selected = true;
            }

            return result;
        }

        public async Task<UserAccountModel?> GetAuthAccountAsync(ClaimsPrincipal account)
        {
            var userAccount = await db.UserAccounts.GetAuthAccountAsync(account);

            UserAccountModel result = null;

            if(userAccount != null)
            {
                result = await GetAccountByIdAsync(userAccount.Id);
            }

            return result;
        }

        public async Task<IdentityResult> RegistrationAsync(UserAccountModel accountModel)
        {
            var account = mapper.Map<UserAccount>(accountModel);

            var result = await db.UserAccounts.RegistrationAsync(account, accountModel.Password);

            await db.UserAccounts.AddToRoleAsync(account, "User");

            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserAccountModel userAccount, string oldPassword, string newPassword)
        {
            var account = mapper.Map<UserAccount>(userAccount);

            var result = await db.UserAccounts.ChangePasswordAsync(account, oldPassword, newPassword);

            return result;
        }

        public async Task<IdentityResult> UpdateAccountAsync(UserAccountModel userAccount)
        {
            var account = await db.UserAccounts.GetByIdAsync(userAccount.Id);

            var updateAccount = mapper.Map<UserAccount>(userAccount);

            await account.EditRoles(userAccount, db);

            account.Edit(updateAccount);

            var result = await db.UserAccounts.UpdateAsync(account);

            if (result.Succeeded)
            {
                await db.SaveAsync();
            }

            return result;
        }

        public async Task<IdentityResult> DeleteAccountAsync(UserAccountModel accountModel)
        {
            var account = await db.UserAccounts.GetByIdAsync(accountModel.Id);
            
            var result = await db.UserAccounts.DeleteAsync(account);

            if (result.Succeeded)
            {
                await db.SaveAsync();
            }

            return result;
        }

        public async Task InitializeAccountsWithRolesAsync()
        {
            var adminProfile = new UserProfile { FirstName = "Admin", LastName = "Adminov" };
            var moderProfile = new UserProfile { FirstName = "Moder", LastName = "Moderov" };
            var userProfile = new UserProfile { FirstName = "User", LastName = "Userov" };

            var adminData = new { Email = "admin@gmail.com", Password = "Adminqwerty", Profile = adminProfile };
            var moderatorData = new { Email = "moderator@gmail.com", Password = "Moderqwerty", Profile = moderProfile };
            var userData = new { Email = "user@gmail.com", Password = "Userqwerty", Profile = userProfile };

            if (await db.RoleManager.FindByNameAsync("Admin") == null)
            {
                await db.RoleManager.CreateAsync(new Role("Admin"));
            }
            if (await db.RoleManager.FindByNameAsync("Moderator") == null)
            {
                await db.RoleManager.CreateAsync(new Role("Moderator"));
            }
            if (await db.RoleManager.FindByNameAsync("User") == null)
            {
                await db.RoleManager.CreateAsync(new Role("User"));
            }

            if (await db.RoleManager.FindByNameAsync(adminData.Email) == null)
            {
                var admin = new UserAccount { Email = adminData.Email, UserName = adminData.Email, Profile = adminData.Profile };
                IdentityResult result = await db.UserAccounts.RegistrationAsync(admin, adminData.Password);
                if (result.Succeeded)
                {
                    var roles = new List<string>() { "User", "Admin"};
                    await db.UserAccounts.AddToRolesAsync(admin, roles);
                }
            }

            if (await db.RoleManager.FindByNameAsync(moderatorData.Email) == null)
            {
                var moderator = new UserAccount { Email = moderatorData.Email, UserName = moderatorData.Email, Profile = moderatorData.Profile };
                IdentityResult result = await db.UserAccounts.RegistrationAsync(moderator, moderatorData.Password);
                if (result.Succeeded)
                {
                    var roles = new List<string>() { "User", "Moderator" };
                    await db.UserAccounts.AddToRolesAsync(moderator, roles);
                }
            }

            if (await db.RoleManager.FindByNameAsync(userData.Email) == null)
            {
                var user = new UserAccount { Email = userData.Email, UserName = userData.Email, Profile = userData.Profile };
                IdentityResult result = await db.UserAccounts.RegistrationAsync(user, userData.Password);
                if (result.Succeeded)
                {
                    await db.UserAccounts.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
