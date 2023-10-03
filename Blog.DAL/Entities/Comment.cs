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
        public UserAccount User { get; set; }

        public string ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
