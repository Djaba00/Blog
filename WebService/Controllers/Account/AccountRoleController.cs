using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.AccountRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Account
{
    [Route("AccountRole")]
    public class AccountRoleController : Controller
    {
        readonly ILogger<AccountRoleController> logger;
        readonly IMapper mapper;
        readonly IAccountRoleService roleService;

        public AccountRoleController(ILogger<AccountRoleController> logger, IMapper mapper, IAccountRoleService roleService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.roleService = roleService;
        }

        [Authorize(Roles = "Admin")]
        [Route("Add")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            logger.LogInformation("GET CreateRole page responsed for user-{0}",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("CreateRole");
        }

        [Authorize(Roles = "Admin")]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateAccountRoleViewModel newRole)
        {
            logger.LogInformation("POST User-{0} send newRole data",
               User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var role = mapper.Map<AccountRoleModel>(newRole);

            var result = await roleService.CreateRoleAsync(role);

            if (result.Succeeded)
            {
                logger.LogInformation("POST User-{0} create new role",
                   User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return RedirectToAction("Roles");
            }

            logger.LogInformation("POST Errors occurred during create role");

            return RedirectToAction("AddRole", "AccountRole");
        }

        [Authorize(Roles = "Admin")]
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleService.GetRoleByIdAsync(id);

            var model = mapper.Map<EditAccountRoleViewModel>(role);

            logger.LogInformation("GET EditRole page responsed for user-{0}",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("EditRole", model);
        }

        [Authorize(Roles = "Admin")]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> EditRoleAsync(CreateAccountRoleViewModel updateRole)
        {
            logger.LogInformation("POST User-{0} send EditRole data",
               User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var role = mapper.Map<AccountRoleModel>(updateRole);

            var result = await roleService.UpdateRoleAsync(role);

            if (result.Succeeded)
            {
                logger.LogInformation("POST User-{0} edited role",
                   User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return RedirectToAction("Roles");
            }

            logger.LogInformation("POST Errors occurred during create role");

            return RedirectToAction("EditRole", "AccountRole");
        }

        [Authorize(Roles = "Admin")]
        [Route("Roles")]
        [HttpGet]
        public async Task<IActionResult> GetAllRolseAsync()
        {
            var roles = await roleService.GetAllRolesAsync();

            var model = new List<AccountRoleViewModel>();

            foreach (var role in roles)
            {
                model.Add(mapper.Map<AccountRoleViewModel>(role));
            }

            logger.LogInformation("GET RoleList page responsed for user-{0}",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("RoleList", model);
        }
    }
}
