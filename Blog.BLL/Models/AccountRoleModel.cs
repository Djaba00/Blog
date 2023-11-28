namespace Blog.BLL.Models
{
    public class AccountRoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }

        public List<UserAccountModel> Accounts { get; set; }
    }
}
