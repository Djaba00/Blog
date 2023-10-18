using Blog.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IAccountRoleService
    {
        Task<IdentityResult> CreateRoleAsync(AccountRoleModel newRole);
        Task<List<AccountRoleModel>> GetAllRolesAsync();
    }
}
