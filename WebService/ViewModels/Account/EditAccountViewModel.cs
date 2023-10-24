using Blog.WebService.ViewModels.AccountRole;
using Blog.WebService.ViewModels.UserProfile;

namespace Blog.WebService.ViewModels.Account
{
    public class EditAccountViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public List<AccountRoleViewModel> Roles { get; set; }

        public UserProfileViewModel Profile { get; set; }

        public AccountRoleViewModel CurrentAccount { get; set; } = new AccountRoleViewModel();
    }
}
