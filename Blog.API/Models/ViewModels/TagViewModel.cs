using System;
namespace Blog.API.Models.ViewModels
{
	public class TagViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ArticleViewModel> Articles { get; set; }
    }
}

