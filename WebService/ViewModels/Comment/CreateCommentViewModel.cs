using Blog.BLL.Models;

namespace Blog.WebService.ViewModels.Comment
{
    public class CreateCommentViewModel
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public int ArticleId { get; set; }
    }
}