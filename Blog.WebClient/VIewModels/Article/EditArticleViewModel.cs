using Blog.BLL.Models;
using Blog.WebClient.VIewModels.Comment;
using Blog.WebClient.VIewModels.Tag;
using Blog.WebClient.VIewModels.User;

namespace Blog.WebClient.VIewModels.Article
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
