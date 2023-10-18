using Blog.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog.BLL.Interfaces
{
    public interface IUserService
    {
        Task<List<UserProfileModel>> GetAllUserProfilesAsync();
        Task<UserProfileModel> GetUserProfileByIdAsync(int id);
        Task<UserProfileModel> GetUserProfileByAccountIdAsync(string id);
        Task CreateUserProfileAsync(UserProfileModel userModel);
        Task UpdateUserProfileAsync(UserProfileModel userModel);
        Task DeleteUserProfileAsync(int id);
    }
}
