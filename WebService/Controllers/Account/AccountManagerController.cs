using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.BLL.Services;
using Blog.WebService.ViewModels.Account;
using Blog.WebService.ViewModels.AccountRole;
using Blog.WebService.ViewModels.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Account
{
    [Route("AccountManager")]
    public class AccountManagerController : Controller
    {
        IMapper mapper;
        IAccountService accountService;
        ISignInService signInService;
        IAccountRoleService roleService;

        public AccountManagerController(IMapper mapper, IAccountService accountService, ISignInService signInService, IAccountRoleService roleService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
            this.signInService = signInService;
            this.roleService = roleService;
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Login")]
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
        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync()
        {
            await signInService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("Accounts")]
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

        [Route("Tag")]
        [HttpGet]
        public async Task<IActionResult> GetAccountsListByTagAsync(string roleName)
        {
            var accounts = await accountService.GetAccountsByRoleAsync(roleName);
            var role = await roleService.GetRoleByNameAsync(roleName);

            var model = new AccountListByRole();

            var roleModel = mapper.Map<AccountRoleViewModel>(role);

            model.AccountRole = roleModel;

            foreach (var user in accounts)
            {
                model.Accounts.Add(mapper.Map<AccountViewModel>(user));
            }

            return View("AccountListByRole", model);
        }

        [Authorize]
        [Route("MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPageAsync()
        {
            var currentUser = await accountService.GetAuthAccountAsync(User);

            var user = await accountService.GetAccountByIdAsync(currentUser.Id);

            var model = mapper.Map<AccountViewModel>(user);

            model.CurrentAccount = model;
            
            return View("UserPage", model);
        }

        [Authorize]
        [Route("UserPage")]
        [HttpGet]
        public async Task<IActionResult> UserPageAsync(string id)
        {
            var currentUser = await accountService.GetAuthAccountAsync(User);

            var user = await accountService.GetAccountByIdAsync(id);

            var model = mapper.Map<AccountViewModel>(user);

            var account = mapper.Map<AccountViewModel>(currentUser);

            model.CurrentAccount = account;

            return View("UserPage", model);
        }

        [Authorize]
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> UpdateUserAsync()
        {
            var currentAccount = await accountService.GetAuthAccountAsync(User);

            var account = await accountService.GetAccountByIdAsync(currentAccount.Id);

            var model = mapper.Map<EditAccountViewModel>(account);

            return View("EditAccount", model);
        }

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserAsync(EditAccountViewModel updateUser)
        {
            var user = mapper.Map<UserAccountModel>(updateUser);

            await accountService.UpdateAccountAsync(user);

            if (updateUser.NewPassword != null && updateUser.OldPassword != null)
            {
                await accountService.ChangePasswordAsync(user, updateUser.OldPassword, updateUser.NewPassword);
            }

            return RedirectToAction("UserPage");
        }

        [Authorize]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteUserProfileAsync(AccountViewModel account)
        {
            var accountModel = mapper.Map<UserAccountModel>(account);
            
            await accountService.DeleteAccountAsync(accountModel);

            return RedirectToAction("Accounts");
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
