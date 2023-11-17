using Blog.API.Models.ViewModels;
using System;
namespace Blog.API.Models.Responses.Account
{
	public class GetAccountResponse : AccountViewModel
	{
        public AccountViewModel CurrentAccount { get; set; } = new AccountViewModel();
    }
}

