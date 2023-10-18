using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Services
{
    public class AccountRoleService : IAccountRoleService
    {
        IUnitOfWork db { get; set; }
        IMapper mapper;

        public AccountRoleService(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public Task<IdentityResult> CreateRoleAsync(AccountRoleModel newRole)
        {
            var role = new Role(newRole.Name, newRole.Description);

            var result = db.RoleManager.CreateAsync(role);

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
    }
}
