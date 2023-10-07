
namespace Blog.BLL.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string About { get; set; }

        public string UserAccountId { get; set; }
        public UserAccountModel UserAccount { get; set; }

        public List<ArticleModel> Articles { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}
