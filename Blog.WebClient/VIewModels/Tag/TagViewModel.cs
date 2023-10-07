using Blog.BLL.Models;
using Blog.WebClient.VIewModels.Article;

namespace Blog.WebClient.VIewModels.Tag
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ArticleViewModel> Articles { get; set; }
    }
}
