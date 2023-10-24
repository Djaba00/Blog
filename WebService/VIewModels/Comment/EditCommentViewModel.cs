using Blog.WebService.ViewModels.Account;

namespace Blog.WebService.ViewModels.Comment
{
    public class EditCommentViewModel
    {
        public int Id {  get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }

        public int ArticleId { get; set; }

        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel();
    }
}
