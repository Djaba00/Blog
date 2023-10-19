using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Account
{
    public class AccountManagerController : Controller
    {
        IMapper mapper;
        IAccountService accountService;
        ISignInService signInService;

        public AccountManagerController(IMapper mapper, IAccountService accountService, ISignInService signInService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
            this.signInService = signInService;
        }

        [Route("AccountManager/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [Route("AccountManager/Login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            var user = mapper.Map<UserAccountModel>(login);

            var result = await signInService.LoginAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                return RedirectToAction("Login", "AccountManager");
            }
        }

        [Authorize]
        [Route("AccountManager/Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync()
        {
            await signInService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("AccountManager/Accounts")]
        [HttpGet]
        public async Task<IActionResult> GetAccountsListAsync()
        {
            var users = await accountService.GetAllAcсountsAsync();

            var models = new List<AccountViewModel>();

            foreach (var user in users)
            {
                models.Add(mapper.Map<AccountViewModel>(user));
            }

            return View("AccountList", models);
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
