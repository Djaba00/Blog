using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebClient.VIewModels.Account;
using Blog.WebClient.VIewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebClient.Controllers.Account
{
    public class AccountManagerController : Controller
    {
        IMapper mapper;
        IAccountService accountService;

        public AccountManagerController(IMapper mapper, IAccountService accountService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<UserAccountModel>(login);

                var result = await accountService.LoginAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }

            return RedirectToAction("Index", "Home"); ;
        }

        [Authorize]
        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync()
        {
            await accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPageAsync()
        {
            var user = User;

            var result = await accountService.GetAuthAccountAsync(user);

            var model = mapper.Map<UserViewModel>(result.Profile);

            // mapper

            return View("UserPage", model);
        }

        [Authorize]
        [Route("Accounts")]
        [HttpGet]
        public async Task<IActionResult> GetAccountsListAsync()
        {
            var users = await accountService.GetAllAcoountsAsync();

            var models = new List<UserViewModel>();

            foreach (var model in models)
            {
                models.Add(mapper.Map<UserViewModel>(model));
            }

            return View("AccountsList", models);
        }

        [Route("InitializeAccounts")]
        [HttpGet]
        public async Task<IActionResult> InitializencAccountsAsync()
        {
            await accountService.InitializeAccountsWithRolesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
