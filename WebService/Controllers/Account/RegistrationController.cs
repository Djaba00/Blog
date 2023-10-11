using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.BLL.Services;
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
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(RegistrationViewModel registration)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<UserAccountModel>(registration);

                var result = await accountService.RegistrationAsync(user);

                if (result.Succeeded)
                {
                    await accountService.LoginAsync(user);

                    return RedirectToAction("Edit", "User");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return RedirectToAction("Index", "Home"); ;
        }
    }
}
