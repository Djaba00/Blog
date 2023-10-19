using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Blog.DAL.Repositories
{
    public class UserAccountRepository : IUserAccountRepository<UserAccount>
    {
        readonly DataContext db;
        readonly UserManager<UserAccount> userManager;
        readonly SignInManager<UserAccount> signInManager;

        public UserAccountRepository(DataContext db, UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IEnumerable<UserAccount>> GetAllAsync()
        {
            var users = await userManager.Users
                .Include(u => u.Profile)
                    .ThenInclude(p => p.Articles)
                .ToListAsync();

            return users;
        }

        public async Task<UserAccount> GetByIdAsync(string id)
        {   
            var user = await userManager.Users
                .Include(u => u.Profile)
                    .ThenInclude(p => p.Articles)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<List<string>> GetUserRolesAsync(UserAccount entity)
        {
            var result = await userManager.GetRolesAsync(entity);

            return result.ToList();
        }

        public async Task<UserAccount> GetAuthAccountAsync(ClaimsPrincipal? userClaims)
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
