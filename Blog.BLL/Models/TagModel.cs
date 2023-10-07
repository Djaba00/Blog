namespace Blog.BLL.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ArticleModel> Articles { get; set; }
    }
}
