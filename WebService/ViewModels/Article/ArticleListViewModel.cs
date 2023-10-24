using Blog.WebService.ViewModels.Account;

namespace Blog.WebService.ViewModels.Article
{
    public class ArticleListViewModel
    {
        public List<ArticleViewModel> Articles { get; set; } = new List<ArticleViewModel>();
        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel();
    }
}
