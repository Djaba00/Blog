using System;
namespace Blog.API.Models.Requests.Comment
{
	public class CreateCommentRequest
	{
        public string Content { get; set; }

        public string UserId { get; set; }

        public int ArticleId { get; set; }
    }
}

