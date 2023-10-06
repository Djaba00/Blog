using Blog.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface IUserAccountRepository<T>
        where T : class
    {
        Task<IEnumerable<UserAccount>> GetAllAsync();
        Task<UserAccount> GetByIdAsync(string id);
        Task<UserAccount> GetAuthAccountAsync(ClaimsPrincipal? userClaims);
        Task<IdentityResult> RegistrationAsync(UserAccount account, string password);
        Task<IdentityResult> UpdateAsync(UserAccount entity);
        Task<IdentityResult> DeleteAsync(string id);
        Task<IdentityResult> ChangePasswordAsync(UserAccount account, string oldPassword, string newPassword);
    }
}
