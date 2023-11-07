using AutoMapper;
using Blog.BLL.Externtions;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.BLL.Services
{
    public class AccountRoleService : IAccountRoleService
    {
        readonly IUnitOfWork db;
        readonly IMapper mapper;

        public AccountRoleService(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IdentityResult> CreateRoleAsync(AccountRoleModel newRole)
        {
            var role = new Role(newRole.Name, newRole.Description);

            var result = await db.RoleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                await db.SaveAsync();
            }

            return result;
        }

        public async Task<IdentityResult> UpdateRoleAsync(AccountRoleModel updRoleModel)
        {
            var role = await db.RoleManager.FindByIdAsync(updRoleModel.Id);

            var updateRole = mapper.Map<Role>(updRoleModel);

            role.Edit(updateRole);

            var result = await db.RoleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                await db.SaveAsync();
            }

            await db.SaveAsync();

            return result;
        }

        public async Task<List<AccountRoleModel>> GetAllRolesAsync()
        {
            var roles = await db.RoleManager.Roles.ToListAsync();

            var result = new List<AccountRoleModel>();

            foreach (var role in roles)
            {
                result.Add(mapper.Map<AccountRoleModel>(role));
            }

            return result;
        }

        public async Task<AccountRoleModel> GetRoleByIdAsync(string id)
        {
            var role = await db.RoleManager.Roles.FirstOrDefaultAsync(r => r.Id == id);

            var result = mapper.Map<AccountRoleModel>(role);

            return result;
        }

        public async Task<AccountRoleModel> GetRoleByNameAsync(string roleName)
        {
            var role = await db.RoleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

            var result = mapper.Map<AccountRoleModel>(role);

            return result;
        }
    }
}
