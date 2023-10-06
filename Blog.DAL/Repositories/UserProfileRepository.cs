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

        public async Task CreateAsync(UserProfile entity)
        {
            await db.UserProfiles.AddAsync(entity);
        }

        public async Task UpdateAsync(UserProfile updateEntitiy)
        {
            var userProfile = await GetByAccountId(updateEntitiy.UserAccountId);

            if (userProfile != null)
            {
                userProfile.FirstName = updateEntitiy.FirstName ?? userProfile.FirstName;
                userProfile.LastName = updateEntitiy.LastName ?? userProfile.LastName;
                userProfile.MiddleName = updateEntitiy.MiddleName ?? userProfile.MiddleName;

                userProfile.BirthDate = updateEntitiy.BirthDate;

                userProfile.Image = updateEntitiy.Image ?? userProfile.Image;
                userProfile.Status = updateEntitiy.Status ?? userProfile.Status;
                userProfile.About = updateEntitiy.About ?? userProfile.About;

                userProfile.Articles = updateEntitiy.Articles ?? userProfile.Articles;
                userProfile.Comments = updateEntitiy.Comments ?? userProfile.Comments;
            }
            else
            {
                throw new Exception("Пользователь не найден");
            }
        }
        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);

            if (user != null)
            {
                db.UserProfiles.Remove(user);
            }
        }
    }
}
