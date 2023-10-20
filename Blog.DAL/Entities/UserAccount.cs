using Microsoft.AspNetCore.Identity;

namespace Blog.DAL.Entities
{
    public class UserAccount : IdentityUser
    {
        public virtual UserProfile Profile { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}