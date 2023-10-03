using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebClient.VIewModels.Account;
using Blog.WebClient.VIewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebClient.Controllers
{
    public class AccountController : Controller
    {
        IMapper mapper;
        IUserService userService;
        
        public AccountController(IMapper mapper, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(RegistrationViewModel registration)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<UserAccountModel>(registration);

                var result = await userService.RegistrationAsync(user);
                if (result.Succeeded)
                {
                    //await userService.LoginAsync(user);

                    return RedirectToAction("MyPage", "AccountManager");
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

        [Route("Login")]
        [HttpPost]
        
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<UserAccountModel>(login);

                var result = await userService.LoginAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
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
            await userService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("User/Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserProfileAsync(EditUserViewModel updateUser)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userService.GetAuthAccountAsync(User);

                if (currentUser.Profile == null)
                {
                    currentUser.Profile = new UserProfileModel();
                }

                currentUser.Profile.FirstName = updateUser.FirstName;
                currentUser.Profile.LastName = updateUser.LastName;
                currentUser.Profile.MiddleName = updateUser.MiddleName;
                currentUser.Profile.BirthDate = updateUser.BirthDate;
                currentUser.Profile.Image = updateUser.Image;
                currentUser.Profile.Status = updateUser.Status;
                currentUser.Profile.About = updateUser.About;

                var result = await userService.UpdateAccountAsync(currentUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
                }
            }

            return RedirectToAction("Index", "Home"); ;
        }
    }
}
