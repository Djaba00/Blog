using Blog.WebService.ViewModels.Account;


namespace Blog.WebService.Interfaces
{
    public interface IRegistrationService
    {
        Task RegistrationAsync(RegistrationViewModel newAccount);
    }
}
