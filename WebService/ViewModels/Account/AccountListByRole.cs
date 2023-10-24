using Blog.WebService.ViewModels.AccountRole;

namespace Blog.WebService.ViewModels.Account
{
    public class AccountListByRole
    {
        public List<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();

        public AccountRoleViewModel AccountRole { get; set; }
    }
}
