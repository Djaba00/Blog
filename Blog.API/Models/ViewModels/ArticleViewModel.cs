using System;
namespace Blog.API.Models.ViewModels
{
	public class ArticleViewModel
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Changed { get; set; }

        public string UserId { get; set; }
        public AccountViewModel User { get; set; }

        public List<TagViewModel> Tags { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}

