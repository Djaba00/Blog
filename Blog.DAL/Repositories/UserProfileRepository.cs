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

        public async Task<UserProfile> GetByIdAsync(string id)
        {
            var user = await db.UserProfiles
                .Include(u => u.Articles)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(u => u.UserAccountId == id);

            return user;
        }

        public async Task CreateAsync(UserProfile entity)
        {
            await db.UserProfiles.AddAsync(entity);
        }

        public void Update(UserProfile entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var user = db.UserProfiles.Find(id);

            if (user != null)
            {
                db.Entry(user).State = EntityState.Deleted;
            }
        }
    }
}
