using AutoMapper;
using Blog.BLL.Exceptions;
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

        public async Task<bool> CanChangeAccount(ClaimsPrincipal claims, string id)
        {
            var currentAccount = await GetAuthAccountAsync(claims);

            if (currentAccount.Id == id || currentAccount.IsInAnyRole("Admin", "Moderator"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserAccountModel userAccount, string oldPassword, string newPassword)
        {
            var account = mapper.Map<UserAccount>(userAccount);

            var result = await db.UserAccounts.ChangePasswordAsync(account, oldPassword, newPassword);

            return result;
        }

        public async Task<UserAccountModel> GetUpdateAccountAsync(ClaimsPrincipal claims, string id)
        {
            try
            {
                var updateAccount = await GetAccountByIdAsync(id);

                var hasPermissions = await CanChangeAccount(claims, id);

                if (!hasPermissions)
                {
                    throw new ForbiddenException();
                }

                return updateAccount;
            }
            catch (ForbiddenException ex)
            {
                logger.LogInformation("ERROR User-{0} doesn't have permissions to update user-{1}",
                    claims.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    id);

                throw new ForbiddenException();
            }
        }

        public async Task<IdentityResult> UpdateAccountAsync(ClaimsPrincipal claims, UserAccountModel userAccount)
        {
            try
            {
                var hasPermissions = await CanChangeAccount(claims, userAccount.Id);

                if (!hasPermissions)
                {
                    throw new ForbiddenException(); 
                }

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
            catch (ForbiddenException ex)
            {
                logger.LogInformation("ERROR User-{0} doesn't have permissions to update user-{1}",
                    claims.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    userAccount.Id);

                throw new ForbiddenException();
            }
        }

        public async Task<IdentityResult> DeleteAccountAsync(ClaimsPrincipal claims, string id)
        {
            try
            {
                var hasPermissions = await CanChangeAccount(claims, id);

                if (!hasPermissions)
                {
                    throw new ForbiddenException();
                }

                var account = await db.UserAccounts.FindAsync(id);

                var result = await db.UserAccounts.DeleteAsync(account);

                if (result.Succeeded)
                {
                    await db.SaveAsync();
                }

                return result;
            }
            catch (ForbiddenException ex)
            {
                logger.LogInformation("ERROR User-{0} doesn't have permissions to delete user-{1}",
                    claims.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    id);

                throw new ForbiddenException();
            }
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
