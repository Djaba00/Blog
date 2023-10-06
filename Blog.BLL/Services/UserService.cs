using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Blog.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork db { get; set; }
        IMapper mapper;

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

            await db.UserProfiles.CreateAsync(userProfile);

            await db.SaveAsync();
        }

        public async Task UpdateUserProfileAsync(UserProfileModel userProfileModel)
        {
            var userProfile = mapper.Map<UserProfile>(userProfileModel);

            await db.UserProfiles.UpdateAsync(userProfile);

            await db.SaveAsync();
        }

        public async Task DeleteUserProfileAsync(int id)
        {
            await db.UserProfiles.DeleteAsync(id);

            await db.SaveAsync();
        }
    }
}
