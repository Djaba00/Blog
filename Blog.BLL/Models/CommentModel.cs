
namespace Blog.BLL.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Changed { get; set; }

        public string UserId { get; set; }
        public UserAccountModel User { get; set; }

        public string ArticleId { get; set; }
        public ArticleModel Article { get; set; }
    }
}
