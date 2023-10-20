using System;

namespace Blog.DAL.Entities
{
	public class Comment
	{
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Changed { get; set; }

        public string UserId { get; set; }
        public virtual UserAccount User { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
