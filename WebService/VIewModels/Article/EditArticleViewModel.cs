using System.ComponentModel.DataAnnotations;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Comment;
using Blog.WebService.ViewModels.Tag;
using Blog.WebService.ViewModels.User;

namespace Blog.WebService.ViewModels.Article
{
    public class EditArticleViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 50)]
        public string Content { get; set; }

        public List<HashTagViewModel> Tags { get; set; }
    }
}
