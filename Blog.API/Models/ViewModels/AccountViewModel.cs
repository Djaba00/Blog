using System;
namespace Blog.API.Models.ViewModels
{
	public class AccountViewModel
	{
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public UserProfileViewModel Profile { get; set; }

        public List<RoleViewModel> Roles { get; set; }

        public List<ArticleViewModel> Articles { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}

