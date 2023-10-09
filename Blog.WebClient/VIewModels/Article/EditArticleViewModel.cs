using Blog.BLL.Models;
using Blog.WebService.VIewModels.Comment;
using Blog.WebService.VIewModels.Tag;
using Blog.WebService.VIewModels.User;

namespace Blog.WebService.VIewModels.Article
{
    public class EditArticleViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }
        public UserViewModel User { get; set; }

        public List<HashTagViewModel> Tags { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}
