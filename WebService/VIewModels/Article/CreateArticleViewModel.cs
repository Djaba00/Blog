using System.ComponentModel.DataAnnotations;
using Blog.WebService.ViewModels.Tag;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.ViewModels.Article
{
    public class CreateArticleViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 50)]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public List<HashTagViewModel> ArticleTags { get; set; } = new List<HashTagViewModel>();
    }
}
