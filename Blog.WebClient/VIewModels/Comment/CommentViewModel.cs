using Blog.BLL.Models;
using Blog.WebClient.VIewModels.Article;
using Blog.WebClient.VIewModels.User;

namespace Blog.WebClient.VIewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Changed { get; set; }

        public string UserId { get; set; }
        public UserViewModel User { get; set; }

        public string ArticleId { get; set; }
        public ArticleViewModel Article { get; set; }
    }
}
