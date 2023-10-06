
namespace Blog.BLL.Models
{
    public class UserAccountModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public List<RoleModel> Roles { get; set; }

        public UserProfileModel Profile { get; set; }
    }
}
