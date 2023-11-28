using Blog.API.Models.ViewModels;
using System;
namespace Blog.API.Models.Responses.Account
{
	public class GetAccountsByRoleResponse
	{
        public List<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();

        public RoleViewModel Role { get; set; }
    }
}

