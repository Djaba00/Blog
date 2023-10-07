using Blog.BLL.Models;
using Blog.WebClient.VIewModels.Tag;
using Blog.WebClient.VIewModels.User;

namespace Blog.WebClient.VIewModels.Article
{
    public class CreateArticleViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public List<HashTagViewModel> Tags { get; set; } = new List<HashTagViewModel>();
    }
}
