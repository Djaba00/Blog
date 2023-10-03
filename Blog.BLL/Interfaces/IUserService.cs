using Blog.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegistrationAsync(UserAccountModel accountModel);
        Task<SignInResult> LoginAsync(UserAccountModel accountModel);
        Task LogoutAsync();

        Task<IdentityResult> UpdateAccountAsync(UserAccountModel accountModel);
        Task<IdentityResult> DeleteAccountAsync(UserAccountModel accountModel);

        Task<List<UserAccountModel>> GetAllUsersAsync();
        Task<UserAccountModel> GetUserByIdAsync(string id);

        Task<UserAccountModel> GetAuthAccountAsync(ClaimsPrincipal? userClaims);
    }
}
