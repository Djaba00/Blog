using Blog.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog.BLL.Interfaces
{
    public interface IAccountRoleService
    {
        Task<IdentityResult> CreateRoleAsync(AccountRoleModel newRole);
        Task<IdentityResult> UpdateRoleAsync(AccountRoleModel updRoleModel);

        Task<List<AccountRoleModel>> GetAllRolesAsync();
        Task<AccountRoleModel> GetRoleByNameAsync(string roleName);
        Task<AccountRoleModel> GetRoleByIdAsync(string id);
    }
}
