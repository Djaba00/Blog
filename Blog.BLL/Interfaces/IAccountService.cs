using Blog.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blog.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<List<UserAccountModel>> GetAllAcсountsAsync();
        Task<List<UserAccountModel>> GetAccountsByRoleAsync(string roleName);
        Task<UserAccountModel> GetAccountByIdAsync(string id);

        Task<UserAccountModel?> GetAuthAccountAsync(ClaimsPrincipal account);
        Task<bool> CanChangeAccount(ClaimsPrincipal claims, string id);

        Task<UserAccountModel> GetUpdateAccountAsync(ClaimsPrincipal claims, string id);
        Task<IdentityResult> UpdateAccountAsync(ClaimsPrincipal claims, UserAccountModel userAccount);
        Task<IdentityResult> ChangePasswordAsync(UserAccountModel userAccount, string oldPassword, string newPassword);
        Task<IdentityResult> DeleteAccountAsync(ClaimsPrincipal claims, string id);

        Task InitializeAccountsWithRolesAsync();
    }
}
