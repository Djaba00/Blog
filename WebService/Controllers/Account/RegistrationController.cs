using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Account
{
    [Route("AccountManager")]
    public class RegistrationController : Controller
    {
        readonly ILogger<RegistrationController> logger;
        readonly IMapper mapper;
        readonly IAccountService accountService;
        readonly ISignInService signInService;

        public RegistrationController(ILogger<RegistrationController> logger, IMapper mapper, IAccountService accountService, ISignInService signInService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.accountService = accountService;
            this.signInService = signInService;
        }

        [Route("Registration")]
        [HttpGet]
        public IActionResult Registration()
        {
            logger.LogInformation("GET Registration page responsed");
            return View("Registration");
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(RegistrationViewModel registration)
        {
            logger.LogInformation("POST send registration data");

            var account = mapper.Map<UserAccountModel>(registration);

            account.Profile = mapper.Map<UserProfileModel>(registration);

            var result = await accountService.RegistrationAsync(account);

            if (result.Succeeded)
            {
                logger.LogInformation("POST A new user has been registered");

                await signInService.LoginAsync(account);

                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                logger.LogInformation("POST Errors occurred during registration");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Registration");
        }
    }
}
