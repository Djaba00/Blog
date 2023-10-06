using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blog.BLL.Services
{
    public class AccountService : IAccountService
    {
        IUnitOfWork db { get; set; }
        IMapper mapper;

        public AccountService(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<UserAccountModel>> GetAllAcoountsAsync()
        {
            var accounts = await db.UserAccounts.GetAllAsync();

            var result = new List<UserAccountModel>();

            foreach (var account in accounts)
            {
                result.Add(mapper.Map<UserAccountModel>(account));
            }

            return result;
        }

        public async Task<UserAccountModel> GetAccountByIdAsync(string id)
        {
            var user = await db.UserAccounts.GetByIdAsync(id);

            var result = mapper.Map<UserAccountModel>(user);

            return result;
        }

        public async Task<UserAccountModel> GetAuthAccountAsync(ClaimsPrincipal account)
        {
            var userAccount = await db.UserAccounts.GetAuthAccountAsync(account);

            var user = await db.UserAccounts.GetByIdAsync(userAccount.Id);

            var result = mapper.Map<UserAccountModel>(user);

            return result;
        }

        public async Task<IdentityResult> RegistrationAsync(UserAccountModel accountModel)
        {
            var account = mapper.Map<UserAccount>(accountModel);

            var result = await db.UserAccounts.RegistrationAsync(account, accountModel.Password);

            return result;
        }

        public async Task<SignInResult> LoginAsync(UserAccountModel accountModel)
        {
            var account = mapper.Map<UserAccount>(accountModel);

            var result = await db.SignInManager.PasswordSignInAsync(account.Email, accountModel.Password, accountModel.RememberMe, false);

            return result;
        }

        public async Task LogoutAsync()
        {
            await db.SignInManager.SignOutAsync();
        }

        public async Task<IdentityResult> DeleteAccountAsync(UserAccountModel accountModel)
        {
            var result = await db.UserAccounts.DeleteAsync(accountModel.Id);

            if (result.Succeeded)
            {
                await db.SaveAsync();
            }

            return result;
        }  
    }
}
