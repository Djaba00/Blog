using Microsoft.AspNetCore.Identity;

namespace Blog.DAL.Entities
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
        public Role(string name) 
            : base(name) 
        { }

        public Role(string name, string description)
            : base(name)
        {
            Description = description;
        }
    }
}
