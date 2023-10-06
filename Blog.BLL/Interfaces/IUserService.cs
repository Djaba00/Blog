using Blog.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
