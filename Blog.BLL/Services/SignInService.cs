﻿using System.Security.Claims;
using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Blog.BLL.Services
{
	public class SignInService : ISignInService
	{
        readonly IUnitOfWork db;
        readonly IMapper mapper;

        public SignInService(IUnitOfWork db, IMapper mapper)
		{
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IdentityResult> RegistrationAsync(UserAccountModel accountModel)
        {
            var account = mapper.Map<UserAccount>(accountModel);

            var result = await db.UserAccounts.RegistrationAsync(account, accountModel.Password);

            await db.UserAccounts.AddToRoleAsync(account, "User");

            return result;
        }

        public async Task<SignInResult> LoginAsync(UserAccountModel accountModel)
        {
            var account = mapper.Map<UserAccount>(accountModel);

            var result = await db.SignInManager.PasswordSignInAsync(account.Email, accountModel.Password, accountModel.RememberMe, false);

            return result;
        }

        public bool IsSignIn(ClaimsPrincipal account)
        {
            var result = db.SignInManager.IsSignedIn(account);

            return result;
        }

        public async Task LogoutAsync()
        {
            await db.SignInManager.SignOutAsync();
        }
    }
}

