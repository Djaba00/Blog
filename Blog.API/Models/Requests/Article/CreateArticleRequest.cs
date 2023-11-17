using Blog.API.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.Requests.Article
{
	public class CreateArticleRequest
	{
		[Required]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 50)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public List<HashTagViewModel> Tags { get; set; } = new List<HashTagViewModel>();

        public List<HashTagViewModel> GetSelectedTags()
        {
            return Tags.Where(c => c.Selected).ToList();
        }
	}
}

