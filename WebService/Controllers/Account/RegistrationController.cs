using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Account
{
    public class RegistrationController : Controller
    {
        IMapper mapper;
        IAccountService accountService;
        ISignInService signInService;

        public RegistrationController(IMapper mapper, IAccountService accountService, ISignInService signInService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
            this.signInService = signInService;
        }

        [Route("Registration")]
        [HttpGet]
        public IActionResult Registration()
        {
            return View("Registration");
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(RegistrationViewModel registration)
        {
            var account = mapper.Map<UserAccountModel>(registration);

            account.Profile = mapper.Map<UserProfileModel>(registration);

            var result = await accountService.RegistrationAsync(account);

            if (result.Succeeded)
            {
                await signInService.LoginAsync(account);

                return RedirectToAction("MyPage", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
