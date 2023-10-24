using Blog.WebService.ViewModels.Account;
using Blog.WebService.ViewModels.Tag;

namespace Blog.WebService.ViewModels.Article
{
    public class ArticleListByTagViewModel
    {
        public string Tag { get; set; }
        public List<ArticleViewModel> Articles { get; set; } = new List<ArticleViewModel>();
        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel(); 
    }
}
