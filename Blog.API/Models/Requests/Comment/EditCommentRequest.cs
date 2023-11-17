using Blog.API.Models.ViewModels;
using System;
namespace Blog.API.Models.Requests.Comment
{
	public class EditCommentRequest
	{
        public int Id { get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }

        public int ArticleId { get; set; }

        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel();
    }
}

