
namespace Blog.BLL.Models
{
    public class UserAccountModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public UserProfileModel Profile { get; set; }

        public List<AccountRoleModel> Roles { get; set; }

        public List<ArticleModel> Articles { get; set; }
        public List<CommentModel> Comments { get; set; }

        public bool IsInAnyRole(params string[] roles)
        {
            foreach (var role in roles)
            {
                if (Roles.Where(r => r.Selected).Select(r => r.Name).Contains(role))
                    return true;
            }

            return false;
        }
    }
}
