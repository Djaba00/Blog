using System;
using Blog.API.Models.ViewModels;

namespace Blog.API.Models.Responses.Account
{
	public class GetAccountsResponse
	{
		public List<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();
	}
}

