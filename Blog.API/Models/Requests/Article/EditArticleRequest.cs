using Blog.API.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.Requests.Article
{
	public class EditArticleRequest
	{
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 50)]
        public string Content { get; set; }

        public List<HashTagViewModel> Tags { get; set; }

        public string UserId { get; set; }

        public AccountViewModel CurrentUser { get; set; } = new AccountViewModel();

        public List<HashTagViewModel> GetSelectedTags()
        {
            return Tags.Where(c => c.Selected).ToList();
        }
    }
}

