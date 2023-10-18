using Blog.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blog.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegistrationAsync(UserAccountModel accountModel);
        Task<SignInResult> LoginAsync(UserAccountModel accountModel);
        Task LogoutAsync();
        Task<IdentityResult> UpdateAccountAsync(UserAccountModel userAccount);
        Task<IdentityResult> ChangePasswordAsync(UserAccountModel userAccount, string oldPassword, string newPassword);
        Task<IdentityResult> DeleteAccountAsync(UserAccountModel accountModel);

        Task<List<UserAccountModel>> GetAllAcoountsAsync();
        Task<UserAccountModel> GetAccountByIdAsync(string id);
        Task<UserAccountModel> GetAuthAccountAsync(ClaimsPrincipal account);

        Task InitializeAccountsWithRolesAsync();
    }
}
