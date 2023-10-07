using Microsoft.AspNetCore.Identity;

namespace Blog.DAL.Entities
{
    public class UserAccount : IdentityUser
    {
        public UserProfile Profile { get; set; }
    }
}