using System;
namespace Blog.DAL.Entities
{
	public class Article
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Content { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Changed { get; set; }

		public string UserId { get; set; }
		public User User { get; set; }

		public virtual ICollection<Tag> Tags { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }
    }
}

