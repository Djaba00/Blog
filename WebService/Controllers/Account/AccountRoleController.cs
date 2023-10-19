using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.AccountRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Account
{
    public class AccountRoleController : Controller
    {
        IMapper mapper;
        IAccountRoleService roleService;

        public AccountRoleController(IMapper mapper, IAccountRoleService roleService)
        {
            this.mapper = mapper;
            this.roleService = roleService;
        }

        [Authorize(Roles = "Admin")]
        [Route("AccountRole/AddRole")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View("CreateRole");
        }

        [Authorize(Roles = "Admin")]
        [Route("AccountRole/AddRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateAccountRoleViewModel newRole)
        {
            var role = mapper.Map<AccountRoleModel>(newRole);

            var result = await roleService.CreateRoleAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("AddRole", "AccountRole");
        }

        [Authorize(Roles = "Admin")]
        [Route("AccountRole/EditRole/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleService.GetRoleByIdAsync(id);

            var model = mapper.Map<EditAccountRoleViewModel>(role);
            
            return View("EditRole", model);
        }

        [Authorize(Roles = "Admin")]
        [Route("AccountRole/AddRole")]
        [HttpPost]
        public async Task<IActionResult> EditRoleAsync(CreateAccountRoleViewModel newRole)
        {
            var role = mapper.Map<AccountRoleModel>(newRole);

            var result = await roleService.UpdateRoleAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("EditRole", "AccountRole");
        }

        [Authorize(Roles = "Admin")]
        [Route("AccountRole/Roles")]
        [HttpGet]
        public async Task<IActionResult> GetAllRolseAsync()
        {
            var roles = await roleService.GetAllRolesAsync();

            var model = new List<AccountRoleViewModel>();

            foreach (var role in roles)
            {
                model.Add(mapper.Map<AccountRoleViewModel>(role));
            }

            return View("RoleList", model);
        }
    }
}
