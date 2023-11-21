using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.DAL.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        DataContext db;
        readonly UserManager<UserAccount> userManager;

        public UserAccountRepository(DataContext db, UserManager<UserAccount> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<UserAccount> FindAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            return user;
        }

        public async Task<IEnumerable<UserAccount>> GetAllAsync()
        {
            var users = await userManager.Users
                .Include(u => u.Profile)
                .Include(u => u.Articles)
                .Include(u => u.Comments)
                .ToListAsync();

            return users;
        }

        public async Task<UserAccount> GetByIdAsync(string id)
        {   
            var user = await userManager.Users
                .Include(u => u.Profile)
                .Include(u => u.Articles)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<List<UserAccount>> GetAccountsByRoleAsync(string roleName)
        {
            var accounts = await userManager.GetUsersInRoleAsync(roleName);

            return accounts.ToList();
        }

        public async Task<List<string>> GetUserRolesAsync(UserAccount entity)
        {
            var result = await userManager.GetRolesAsync(entity);

            return result.ToList();
        }

        public async Task<UserAccount?> GetAuthAccountAsync(ClaimsPrincipal? userClaims)
        {
            var result = await userManager.GetUserAsync(userClaims);

            return result;
        }

        public async Task<IdentityResult> UpdateAsync(UserAccount entity)
        {
            var result = await userManager.UpdateAsync(entity);

            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserAccount account, string oldPassword, string newPassword)
        {
            var result = await userManager.ChangePasswordAsync(account, oldPassword, newPassword);

            return result;
        }

        public async Task<IdentityResult> DeleteAsync(UserAccount entity)
        {            
            var result = await userManager.DeleteAsync(entity);

            return result;
        }

        public async Task<IdentityResult> RegistrationAsync(UserAccount account, string password)
        {

            var result = await userManager.CreateAsync(account, password);

            return result;
        }

        public async Task<IdentityResult> AddToRoleAsync(UserAccount account, string role)
        {
            var result = await userManager.AddToRoleAsync(account, role);

            return result;
        }

        public async Task<IdentityResult> AddToRolesAsync(UserAccount account, List<string> roles)
        {
            var result = await userManager.AddToRolesAsync(account, roles);

            return result;
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(UserAccount account, string role)
        {
            var result = await userManager.RemoveFromRoleAsync(account, role);

            return result;
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(UserAccount account, List<string> roles)
        {
            var result = await userManager.RemoveFromRolesAsync(account, roles);

            return result;
        }
    }
}
