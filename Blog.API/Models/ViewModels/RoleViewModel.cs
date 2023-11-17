using System;
namespace Blog.API.Models.ViewModels
{
	public class RoleViewModel
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }

        public List<AccountViewModel> Accounts { get; set; }
    }
}

