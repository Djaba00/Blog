using Blog.BLL.Models;
using Blog.WebService.ViewModels.Account;
using Blog.WebService.ViewModels.Article;
using Blog.WebService.ViewModels.Comment;

namespace Blog.WebService.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string About { get; set; }

        public string UserAccountId { get; set; }

        public string GetFullName()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    } 
}
