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
            logger.LogInformation("{0} GET Registration page responsed",
                DateTime.UtcNow.ToLongTimeString());
            return View("Registration");
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(RegistrationViewModel registration)
        {
            logger.LogInformation("{0} POST send registration data",
                DateTime.UtcNow.ToLongTimeString());

            var account = mapper.Map<UserAccountModel>(registration);

            account.Profile = mapper.Map<UserProfileModel>(registration);

            var result = await accountService.RegistrationAsync(account);

            if (result.Succeeded)
            {
                logger.LogInformation("{0} POST A new user has been registered",
                DateTime.UtcNow.ToLongTimeString());

                await signInService.LoginAsync(account);

                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                logger.LogInformation("{0} POST Errors occurred during registration",
                DateTime.UtcNow.ToLongTimeString());

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Registration");
        }
    }
}
