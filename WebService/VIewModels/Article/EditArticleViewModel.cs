using System.ComponentModel.DataAnnotations;
using Blog.BLL.Models;
using Blog.WebService.VIewModels.Comment;
using Blog.WebService.VIewModels.Tag;
using Blog.WebService.VIewModels.User;

namespace Blog.WebService.VIewModels.Article
{
    public class EditArticleViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 50)]
        public string Content { get; set; }

        public string UserId { get; set; }
        public UserViewModel User { get; set; }

        public List<HashTagViewModel> Tags { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}
