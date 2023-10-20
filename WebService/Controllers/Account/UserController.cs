using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Account
{
    public class UserController : Controller
    {
        IMapper mapper;
        readonly IUserService userService;
        readonly IAccountService accountService;

        public UserController(IMapper mapper, IUserService userService, IAccountService accountService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.accountService = accountService;
        }


        [Authorize]
        [Route("User/MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPageAsync()
        {
            var currentUser = await accountService.GetAuthAccountAsync(User);

            var user = await accountService.GetAccountByIdAsync(currentUser.Id);

            var model = mapper.Map<UserViewModel>(user.Profile);

            return View("UserPage", model);
        }

        [Authorize]
        [Route("User/EditUser")]
        [HttpGet]
        public async Task<IActionResult> UpdateUserAsync()
        {
            var account = await accountService.GetAuthAccountAsync(User);

            account.Profile = await userService.GetUserProfileByAccountIdAsync(account.Id);

            var model = mapper.Map<EditUserViewModel>(account);

            return View("EditUser", model);
        }

        [Authorize]
        [Route("User/EditUser")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserAsync(EditUserViewModel updateUser)
        {
            var user = mapper.Map<UserAccountModel>(updateUser);

            await accountService.UpdateAccountAsync(user);

            if(updateUser.NewPassword != null && updateUser.OldPassword != null)
            {
                await accountService.ChangePasswordAsync(user, updateUser.OldPassword, updateUser.NewPassword);
            }

            return RedirectToAction("MyPage");
        }

        [Authorize]
        [Route("User/DeleteUser")]
        [HttpPost]
        public async Task<IActionResult> DeleteUserProfileAsync(int id)
        {
            await userService.DeleteUserProfileAsync(id);

            return RedirectToAction("MyPage");
        }

        [Route("User/Users")]
        [HttpGet]
        public async Task<IActionResult> GetUserProfilesListAsync()
        {
            var users = await userService.GetAllUserProfilesAsync();

            var models = new List<UserViewModel>();

            foreach (var user in users)
            {
                models.Add(mapper.Map<UserViewModel>(user));
            }

            return View("AccountsList", models);
        }

        [Authorize]
        [Route("User/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetUserProfileByIdAsync(int id)
        {
            var user = await userService.GetUserProfileByIdAsync(id);

            var model = mapper.Map<UserViewModel>(user);

            return View("AccountsList", model);
        }
    }
}
