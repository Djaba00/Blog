﻿using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Account;
using Blog.WebService.ViewModels.AccountRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Account
{
    [Route("AccountManager")]
    public class AccountManagerController : Controller
    {
        readonly ILogger<AccountManagerController> logger;
        readonly IMapper mapper;
        readonly IAccountService accountService;
        readonly ISignInService signInService;
        readonly IAccountRoleService roleService;

        public AccountManagerController(ILogger<AccountManagerController> logger, IMapper mapper, IAccountService accountService, ISignInService signInService, IAccountRoleService roleService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.accountService = accountService;
            this.signInService = signInService;
            this.roleService = roleService;
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            logger.LogInformation("GET The Login page is rquested");

            return View();
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            if(ModelState.IsValid)
            {
                logger.LogInformation("POST User send Login data");

                var user = mapper.Map<UserAccountModel>(login);

                var result = await signInService.LoginAsync(user);

                if (result.Succeeded)
                {
                    logger.LogInformation("{0} POST Login succesfull");

                    return RedirectToAction("MyPage");
                }
                else
                {
                    logger.LogInformation("{0} POST Errors occurred during login");

                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    return RedirectToAction("Login");
                }
            }
            logger.LogInformation("{0} POST Errors occurred during login");

            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            return RedirectToAction("Login");
        }

        [Authorize]
        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await signInService.LogoutAsync();
            
            logger.LogInformation("POST Logout user-{0} successfull",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

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

            logger.LogInformation("GET Accounts page responsed");

            return View("AccountList", models);
        }

        [Route("Role")]
        [HttpGet]
        public async Task<IActionResult> GetAccountsListByRoleAsync(string roleName)
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

            logger.LogInformation("GET Accounts page by {0} role responsed}",
                roleName);

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

            logger.LogInformation("GET User page responsed for user-{0}",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("UserPage", model);
        }

        [Authorize]
        [Route("UserPage")]
        [HttpGet]
        public async Task<IActionResult> UserPageAsync(string id)
        {
            logger.LogInformation("GET user-{0} requested User-{1} page",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                id);

            var currentUser = await accountService.GetAuthAccountAsync(User);

            var user = await accountService.GetAccountByIdAsync(id);

            var model = mapper.Map<AccountViewModel>(user);

            var account = mapper.Map<AccountViewModel>(currentUser);

            model.CurrentAccount = account;

            logger.LogInformation("GET User-{0} page responsed for user-{1}",
                id,
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("UserPage", model);
        }

        [Authorize]
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> UpdateUserAsync()
        {
            logger.LogInformation("GET user-{0} requested EditAccount page",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var currentAccount = await accountService.GetAuthAccountAsync(User);

            var account = await accountService.GetAccountByIdAsync(currentAccount.Id);

            var model = mapper.Map<EditAccountViewModel>(account);

            logger.LogInformation("GET EditAccount page responsed for user-{0}",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("EditAccount", model);
        }

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserAsync(EditAccountViewModel updateUser)
        {
            logger.LogInformation("POST user-{0} send EditAccount data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var user = mapper.Map<UserAccountModel>(updateUser);

            await accountService.UpdateAccountAsync(user);

            if (updateUser.NewPassword != null && updateUser.OldPassword != null)
            {
                var result = await accountService.ChangePasswordAsync(user, updateUser.OldPassword, updateUser.NewPassword);

                if(result.Errors.Count() != 0)
                {
                    ModelState.AddModelError("", result.Errors.FirstOrDefault().Description);

                    logger.LogInformation("POST user-{0} account update error - {1}",
                        User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                        result.Errors.FirstOrDefault().Description);

                    return View("EditAccount", updateUser);
                }
            }

            logger.LogInformation("POST user-{0} account update successfull",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return RedirectToAction("MyPage");
        }

        [Authorize]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteUserProfileAsync(AccountViewModel account)
        {
            logger.LogInformation("POST user-{0} send delete account data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var accountModel = mapper.Map<UserAccountModel>(account);
            
            await accountService.DeleteAccountAsync(accountModel);

            logger.LogInformation("POST user-{0} delete user-{1}",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                account.Id);

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
