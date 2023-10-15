using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.BLL.Services;
using Blog.WebService.Interfaces;
using Blog.WebService.VIewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Account
{
    public class RegistrationController : Controller
    {
        IMapper mapper;
        IAccountService accountService;

        public RegistrationController(IMapper mapper, IAccountService accountService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
        }

        [Route("Registration")]
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
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
                await accountService.LoginAsync(account);

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
