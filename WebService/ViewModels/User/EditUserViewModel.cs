﻿using Blog.WebService.ViewModels.AccountRole;
using System.ComponentModel.DataAnnotations;

namespace Blog.WebService.ViewModels.User
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public List<AccountRoleViewModel> Roles { get; set; }

        public UserViewModel Profile { get; set; }
    }
}
