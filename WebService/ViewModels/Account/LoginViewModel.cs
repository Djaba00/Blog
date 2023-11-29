using System.ComponentModel.DataAnnotations;

namespace Blog.WebService.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}