using Blog.API.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.Requests.Account
{
	public class EditAccountRequest
	{
        [Required]
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public List<RoleViewModel> Roles { get; set; }

        public UserProfileViewModel Profile { get; set; }

        [Required]
        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel();
    }
}

