using Blog.BLL.Models;

namespace Blog.WebService.VIewModels.Comment
{
    public class CreateCommentViewModel
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public string ArticleId { get; set; }
    }
}
