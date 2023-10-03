
namespace Blog.BLL.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Changed { get; set; }

        public string UserId { get; set; }
        public UserAccountModel User { get; set; }

        public virtual ICollection<TagModel> Tags { get; set; }

        public virtual ICollection<CommentModel> Comments { get; set; }
    }
}
