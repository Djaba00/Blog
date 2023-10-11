using Blog.BLL.Models;
using Blog.WebService.VIewModels.Article;

namespace Blog.WebService.VIewModels.Tag
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ArticleViewModel> Articles { get; set; }
    }
}
