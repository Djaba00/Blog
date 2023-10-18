using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.DAL.Repositories
{
    public class UserAccountRepository : IUserAccountRepository<UserAccount>
    {
        readonly DataContext db;
        readonly UserManager<UserAccount> userManager;

        public UserAccountRepository(DataContext db, UserManager<UserAccount> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<UserAccount>> GetAllAsync()
        {
            var users = await userManager.Users
                .Include(u => u.Profile)
                .ToListAsync();

            return users;
        }

        public async Task<UserAccount> GetByIdAsync(string id)
        {   
            var user = await userManager.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public Task<UserAccount> GetAuthAccountAsync(ClaimsPrincipal? userClaims)
        {
            var userId = userManager.GetUserId(userClaims);

            var result = userManager.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return result;
        }

        public async Task<IdentityResult> UpdateAsync(UserAccount entity)
        {
            var result = await userManager.UpdateAsync(entity);

            return result;
        }

        public async Task<IdentityResult> DeleteAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            
            var result = await userManager.DeleteAsync(user);

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

        public async Task<IdentityResult> ChangePasswordAsync(UserAccount account, string oldPassword, string newPassword)
        {
            var result = await userManager.ChangePasswordAsync(account, oldPassword, newPassword);

            return result;
        }
    }
}
