﻿using Blog.WebService.ViewModels.AccountRole;
using Blog.WebService.ViewModels.Article;
using Blog.WebService.ViewModels.Comment;
using Blog.WebService.ViewModels.UserProfile;

namespace Blog.WebService.ViewModels.Account
{
    public class UserPageViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public UserProfileViewModel Profile { get; set; }

        public List<AccountRoleViewModel> Roles { get; set; }

        public List<ArticleViewModel> Articles { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel();
    }
}
