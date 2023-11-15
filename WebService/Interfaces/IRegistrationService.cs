using Blog.WebService.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Interfaces
{
    public interface IRegistrationService
    {
        Task<IdentityResult> RegistrationAsync(RegistrationViewModel newAccount);
    }
}
