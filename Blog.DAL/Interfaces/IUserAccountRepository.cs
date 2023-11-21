using Blog.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blog.DAL.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<UserAccount> FindAsync(string id);
        Task<IEnumerable<UserAccount>> GetAllAsync();
        Task<List<UserAccount>> GetAccountsByRoleAsync(string roleName);
        Task<UserAccount> GetByIdAsync(string id);
        Task<UserAccount?> GetAuthAccountAsync(ClaimsPrincipal? userClaims);
        Task<List<string>> GetUserRolesAsync(UserAccount entity);

        Task<IdentityResult> RegistrationAsync(UserAccount account, string password);

        Task<IdentityResult> UpdateAsync(UserAccount entity);
        Task<IdentityResult> DeleteAsync(UserAccount entity);
        Task<IdentityResult> ChangePasswordAsync(UserAccount account, string oldPassword, string newPassword);

        Task<IdentityResult> AddToRoleAsync(UserAccount account, string role);
        Task<IdentityResult> AddToRolesAsync(UserAccount account, List<string> roles);
        Task<IdentityResult> RemoveFromRoleAsync(UserAccount account, string role);
        Task<IdentityResult> RemoveFromRolesAsync(UserAccount account, List<string> roles);
    }
}
