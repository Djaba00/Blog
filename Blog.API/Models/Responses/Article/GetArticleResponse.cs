using Blog.API.Models.ViewModels;
using System;
namespace Blog.API.Models.Responses.Article
{
	public class GetArticleResponse : ArticleViewModel
	{
        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel();
    }
}

