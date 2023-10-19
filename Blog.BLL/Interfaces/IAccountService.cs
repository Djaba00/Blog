using Blog.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blog.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<List<UserAccountModel>> GetAllAcсountsAsync();
        Task<UserAccountModel> GetAccountByIdAsync(string id);
        Task<UserAccountModel> GetAuthAccountAsync(ClaimsPrincipal account);

        Task<IdentityResult> RegistrationAsync(UserAccountModel accountModel);

        Task<IdentityResult> UpdateAccountAsync(UserAccountModel userAccount);
        Task<IdentityResult> ChangePasswordAsync(UserAccountModel userAccount, string oldPassword, string newPassword);
        Task<IdentityResult> DeleteAccountAsync(UserAccountModel accountModel);

        Task InitializeAccountsWithRolesAsync();
    }
}
