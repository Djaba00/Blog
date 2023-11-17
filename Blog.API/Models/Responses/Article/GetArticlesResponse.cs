using Blog.API.Models.ViewModels;
using System;
namespace Blog.API.Models.Responses.Article
{
	public class GetArticlesResponse
	{
        public List<ArticleViewModel> Articles { get; set; } = new List<ArticleViewModel>();
        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel();
    }
}

