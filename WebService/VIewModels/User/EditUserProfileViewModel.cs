using System.ComponentModel.DataAnnotations;

namespace Blog.WebService.VIewModels.User
{
    public class EditUserProfileViewModel
    {
        public string UserAccountId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string About { get; set; }
    }
}
