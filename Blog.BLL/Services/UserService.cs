using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork db;
        IMapper mapper;

        public UserService(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
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

            var result = await db.AccountSignIn.PasswordSignInAsync(account.Email, accountModel.Password, accountModel.RememberMe, false);

            return result;
        }

        public async Task LogoutAsync()
        {
            await db.AccountSignIn.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateAccountAsync(UserAccountModel accountModel)
        {
            var account = mapper.Map<UserAccount>(accountModel);

            var result = await db.UserAccounts.UpdateAsync(account);

            if (result.Succeeded)
            {
                await db.SaveAsync();
            }

            return result;
        }

        public async Task<IdentityResult> DeleteAccountAsync(UserAccountModel accountModel)
        {
            var account = mapper.Map<UserAccount>(accountModel);

            var result = await db.UserAccounts.DeleteAsync(account.Id);

            if (result.Succeeded)
            {
                await db.SaveAsync();
            }

            return result;
        }

        public async Task<List<UserAccountModel>> GetAllUsersAsync()
        {
            var users = await db.UserProfiles.GetAllAsync();

            var result = new List<UserAccountModel>();

            foreach (var user in users)
            {
                result.Add(mapper.Map<UserAccountModel>(user));
            }

            return result;
        }

        public async Task<UserAccountModel> GetUserByIdAsync(string id)
        {
            var user = await db.UserAccounts.GetByIdAsync(id);

            var result = mapper.Map<UserAccountModel>(user);

            return result;
        }

        public async Task<UserAccountModel> GetAuthAccountAsync(ClaimsPrincipal? userClaims)
        {
            var user = await db.UserAccounts.GetAuthAccountAsync(userClaims);

            var result = mapper.Map<UserAccountModel>(user);

            return result;
        }
    }
}
