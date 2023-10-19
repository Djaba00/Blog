using Blog.BLL.Models;
using Blog.WebService.ViewModels.Article;

namespace Blog.WebService.ViewModels.Tag
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ArticleViewModel> Articles { get; set; }
    }
}
