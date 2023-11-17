using Blog.WebService.ViewModels.Account;

namespace Blog.WebService.ViewModels.AccountRole
{
    public class AccountRoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }

        public List<AccountViewModel> Accounts { get; set; }
    }
}
