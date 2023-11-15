using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using System;

namespace Blog.WebService.Services
{
    public class SignInControllerService
    {
        IMapper mapper;
        IAccountService accountService { get; set; }
        ISignInService signInService { get; set; }

        public SignInControllerService(IMapper mapper, IAccountService accountService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task<IdentityResult> RegistrationAsync(RegistrationViewModel newAccount)
        {
            var account = mapper.Map<UserAccountModel>(newAccount);

            account.Profile = mapper.Map<UserProfileModel>(newAccount);

            var result = await accountService.RegistrationAsync(account);

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel login)
        {
            var user = mapper.Map<UserAccountModel>(login);

            var result = await signInService.LoginAsync(user);

            return result;
        }
    }
}
