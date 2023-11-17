using Blog.BLL.Models;
using Blog.WebService.ViewModels.Account;
using Blog.WebService.ViewModels.Comment;
using Blog.WebService.ViewModels.Tag;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.WebService.ViewModels.Article
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Changed { get; set; }

        public string UserId { get; set; }
        public AccountViewModel User { get; set; }
        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel();

        public List<TagViewModel> Tags { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public CreateCommentViewModel AddComment { get; set; }
    }
}
