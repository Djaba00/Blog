using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Repositories
{
    public class UserProfileRepository : IUserProfileRepository<UserProfile>
    {
        DataContext db;

        public UserProfileRepository(DataContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            var users = await db.UserProfiles
                .Include(u => u.Articles)
                .Include(u => u.Comments)
                .ToListAsync();

            return users;
        }

        public async Task<UserProfile> GetByIdAsync(int id)
        {
            var user = await db.UserProfiles
                .Include(u => u.Articles)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<UserProfile> GetByAccountId(string id)
        {
            var user = await db.UserProfiles
                .Include(u => u.Articles)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(u => u.UserAccountId == id);

            return user;
        }

        public void Create(UserProfile entity)
        {
            db.UserProfiles.Add(entity);
        }

        public void Update(UserProfile updateEntitiy)
        {
            db.UserProfiles.Update(updateEntitiy);
        }
        public void Delete(UserProfile entity)
        {
            db.UserProfiles.Remove(entity);
        }
    }
}
