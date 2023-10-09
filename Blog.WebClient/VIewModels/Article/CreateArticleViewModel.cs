using Blog.BLL.Models;
using Blog.WebService.VIewModels.Tag;
using Blog.WebService.VIewModels.User;

namespace Blog.WebService.VIewModels.Article
{
    public class CreateArticleViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public List<HashTagViewModel> Tags { get; set; } = new List<HashTagViewModel>();
    }
}
