using Blog.WebService.ViewModels.Account;
using Blog.WebService.ViewModels.Article;

namespace Blog.WebService.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Changed { get; set; }

        public string UserId { get; set; }
        public AccountViewModel User { get; set; }

        public int ArticleId { get; set; }
        public ArticleViewModel Article { get; set; }
    }
}