using System;
using Microsoft.AspNetCore.Identity;

namespace Blog.DAL.Entities
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string About { get; set; }

		public virtual ICollection<Article> Articles { get; set; }
		public virtual ICollection<Comment> Comments { get; set; } 
    }
}

