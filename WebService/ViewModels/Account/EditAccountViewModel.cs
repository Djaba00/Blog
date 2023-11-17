using System.ComponentModel.DataAnnotations;
using Blog.WebService.ViewModels.AccountRole;
using Blog.WebService.ViewModels.UserProfile;

namespace Blog.WebService.ViewModels.Account
{
    public class EditAccountViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public List<AccountRoleViewModel> Roles { get; set; }

        public UserProfileViewModel Profile { get; set; }

        [Required]
        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel();
    }
}
