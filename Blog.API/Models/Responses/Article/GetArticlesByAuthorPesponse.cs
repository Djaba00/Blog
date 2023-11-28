using System;
using Blog.API.Models.ViewModels;

namespace Blog.API.Models.Responses.Article
{
	public class GetArticlesByAuthorPesponse
	{
		public List<ArticleViewModel> Articles { get; set; }
	}
}

