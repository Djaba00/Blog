﻿using Blog.BLL.Models;
using Blog.WebService.ViewModels.AccountRole;
using Blog.WebService.ViewModels.User;

namespace Blog.WebService.ViewModels.Account
{
    public class AccountViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public UserViewModel Profile { get; set; }

        public List<AccountRoleViewModel> Roles { get; set; }
    }
}