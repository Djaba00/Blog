using Microsoft.AspNetCore.Identity;

namespace Blog.DAL.Entities
{
    public class Role : IdentityRole
    {
        public Role() : base() { }

        public Role(string name)
            : base(name)
        { }
    }
}
