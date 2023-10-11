using System.ComponentModel.DataAnnotations;
using Blog.BLL.Models;
using Blog.WebService.VIewModels.Tag;
using Blog.WebService.VIewModels.User;

namespace Blog.WebService.VIewModels.Article
{
    public class CreateArticleViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 50)]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public List<HashTagViewModel> Tags { get; set; } = new List<HashTagViewModel>();
    }
}
