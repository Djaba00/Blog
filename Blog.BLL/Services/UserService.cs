using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;

namespace Blog.BLL.Services
{
    public class UserService : IUserService
    {
        readonly IUnitOfWork db;
        readonly IMapper mapper;

        public UserService(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<UserProfileModel>> GetAllUserProfilesAsync()
        {
            var userProfiles = await db.UserProfiles.GetAllAsync();

            var result = new List<UserProfileModel>();

            foreach (var userProfile in userProfiles)
            {
                result.Add(mapper.Map<UserProfileModel>(userProfile));
            }

            return result;
        }

        public async Task<UserProfileModel> GetUserProfileByIdAsync(int id)
        {
            var userProfile = await db.UserProfiles.GetByIdAsync(id);

            var result = mapper.Map<UserProfileModel>(userProfile);

            return result;
        }

        public async Task<UserProfileModel> GetUserProfileByAccountIdAsync(string id)
        {
            var userProfile = await db.UserProfiles.GetByAccountId(id);

            var result = mapper.Map<UserProfileModel>(userProfile);

            return result;
        }

        public async Task CreateUserProfileAsync(UserProfileModel userModel)
        {
            var userProfile = mapper.Map<UserProfile>(userModel);

            db.UserProfiles.Create(userProfile);

            await db.SaveAsync();
        }

        public async Task UpdateUserProfileAsync(UserProfileModel userProfileModel)
        {
            var userProfile = mapper.Map<UserProfile>(userProfileModel);

            db.UserProfiles.Update(userProfile);

            await db.SaveAsync();
        }

        public async Task DeleteUserProfileAsync(int id)
        {
            var user = await db.UserProfiles.GetByIdAsync(id);
            
            db.UserProfiles.Delete(user);

            await db.SaveAsync();
        }
    }
}
