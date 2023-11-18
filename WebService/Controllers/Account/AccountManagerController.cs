using System.Security.Claims;
using AutoMapper;
using Blog.BLL.Exceptions;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
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
        public async Task<IActionResult> GetAccountListByRoleAsync(string roleName)
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

            logger.LogInformation("GET Accounts page by {0} role responsed",
                roleName);

            return View("AccountListByRole", model);
        }

        [Authorize]
        [Route("MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPageAsync()
        {
            var currentAccount = await accountService.GetAuthAccountAsync(User);

            var model = mapper.Map<UserPageViewModel>(currentAccount);

            var account = mapper.Map<AccountViewModel>(currentAccount);

            model.CurrentAccount = account;

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

            var currentAccount = await accountService.GetAuthAccountAsync(User);

            var user = await accountService.GetAccountByIdAsync(id);

            var account = mapper.Map<AccountViewModel>(currentAccount);

            var model = mapper.Map<UserPageViewModel>(user);

            model.CurrentAccount = account;

            logger.LogInformation("GET User-{0} page responsed for user-{1}",
                id,
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("UserPage", model);
        }

        [Authorize]
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> UpdateUserAsync(string id)
        {
            try
            {
                logger.LogInformation("GET user-{0} requested EditAccount page",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                var updateAccount = new UserAccountModel();

                if (id == null)
                {
                    updateAccount = await accountService.GetAuthAccountAsync(User);
                }
                else
                {
                    updateAccount = await accountService.GetUpdateAccountAsync(User, id);
                }

                var model = mapper.Map<EditAccountViewModel>(updateAccount);

                logger.LogInformation("GET EditAccount page responsed for user-{0}",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return View("EditAccount", model);
            }
            catch (ForbiddenException ex)
            {
                return RedirectToAction("403", "Error");
            }
        }

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserAsync(EditAccountViewModel updateUser)
        {
            try
            {
                logger.LogInformation("POST user-{0} send EditAccount data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                var currentAccount = await accountService.GetAuthAccountAsync(User);

                var user = mapper.Map<UserAccountModel>(updateUser);

                await accountService.UpdateAccountAsync(User, user);

                if (updateUser.NewPassword != null && updateUser.OldPassword != null)
                {
                    var result = await accountService.ChangePasswordAsync(user, updateUser.OldPassword, updateUser.NewPassword);
                }

                logger.LogInformation("POST user-{0} account update successfull",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return RedirectToAction("MyPage");
            }
            catch (ForbiddenException ex)
            {
                return RedirectToAction("403", "Error");
            }
        }

        [Authorize]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteUserAsync(AccountViewModel account)
        {
            try
            {
                logger.LogInformation("POST user-{0} send delete account data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                var accountModel = mapper.Map<UserAccountModel>(account);

                await accountService.DeleteAccountAsync(User, accountModel);

                logger.LogInformation("POST user-{0} delete user-{1}",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    account.Id);

                return RedirectToAction("Accounts");
            }
            catch (ForbiddenException ex)
            {
                return RedirectToAction("403", "Error");
            }
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
