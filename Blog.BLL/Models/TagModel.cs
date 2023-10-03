
namespace Blog.BLL.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ArticleModel> Articles { get; set; }
    }
}
