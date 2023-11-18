using System;
using System.Security.Claims;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog.BLL.Interfaces
{
	public interface ISignInService
	{
        Task<IdentityResult> RegistrationAsync(UserAccountModel accountModel);
        Task<SignInResult> LoginAsync(UserAccountModel accountModel);
        bool IsSignIn(ClaimsPrincipal account);
        Task LogoutAsync();
    }
}

