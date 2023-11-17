using Blog.API.Models.ViewModels;
using System;
namespace Blog.API.Models.Responses.AccountRole
{
	public class GetRolesResponse
	{
		public List<RoleViewModel> Roles { get; set; }
	}
}

