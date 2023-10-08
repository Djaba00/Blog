using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebClient.VIewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebClient.Controllers.Account
{
    public class UserController : Controller
    {
        IMapper mapper;
        IUserService userService;
        IAccountService accountService;

        public UserController(IMapper mapper, IUserService userService, IAccountService accountService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.accountService = accountService;
        }

        [Authorize]
        [Route("NewProfile")]
        [HttpPost]
        public async Task<IActionResult> CreateUserProfileAsync(EditUserProfileViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var userProfile = mapper.Map<UserProfileModel>(userModel);

                await userService.CreateUserProfileAsync(userProfile);

                return RedirectToAction("MyPage");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("EditProfile")]
        [HttpGet]
        public async Task<IActionResult> UpdateUserProfileAsync()
        {
            var user = User;

            var result = await accountService.GetAuthAccountAsync(user);

            var editModel = mapper.Map<EditUserProfileViewModel>(result);

            return View("UserEdit", editModel);
        }

        [Authorize]
        [Route("EditProfile")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserProfileAsync(EditUserProfileViewModel updateUser)
        {
            if (ModelState.IsValid)
            {
                var userProfile = mapper.Map<UserProfileModel>(updateUser);

                await userService.UpdateUserProfileAsync(userProfile);

                return RedirectToAction("MyPage");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("DeleteProfile")]
        [HttpPost]
        public async Task<IActionResult> DeleteUserProfileAsync(int id)
        {
            await userService.DeleteUserProfileAsync(id);

            return RedirectToAction("MyPage");
        }

        [Authorize(Roles = "Admin")]
        [Route("Users")]
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
