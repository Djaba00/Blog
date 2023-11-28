using AutoMapper;
using Blog.API.Models.Requests.Role;
using Blog.API.Models.Responses.AccountRole;
using Blog.API.Models.ViewModels;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers.Account
{
	public class RoleController
	{
        [Produces("application/json")]
        [Route("api/[controller]")]
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

            /// <summary>
            /// Create role
            /// </summary>
            /// <remarks>
            /// Sample request:
            /// POST /Add
            /// </remarks>
            /// <response code="200">Success</response>
            /// <response code="400">If invalid request</response>
            /// <response code="403">If the user doesn't have rights</response>
            [Authorize(Roles = "Admin")]
            [Route("Add")]
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status403Forbidden)]
            public async Task<IActionResult> CreateRoleAsync(CreatedAtRouteResult newRole)
            {
                logger.LogInformation("POST User-{0} send newRole data",
                   User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                var role = mapper.Map<AccountRoleModel>(newRole);

                var result = await roleService.CreateRoleAsync(role);

                if (result.Succeeded)
                {
                    logger.LogInformation("POST User-{0} create new role",
                       User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                    return Ok();
                }

                logger.LogInformation("POST Errors occurred during create role");

                return BadRequest();
            }

            /// <summary>
            /// Edit role
            /// </summary>
            /// <remarks>
            /// Sample request:
            /// POST /Edit
            /// </remarks>
            /// <response code="200">Success</response>
            /// <response code="400">If invalid request</response>
            /// <response code="403">If the user doesn't have rights</response>
            [Authorize(Roles = "Admin")]
            [HttpPut]
            public async Task<IActionResult> EditRoleAsync(CreateRoleRequest updateRole)
            {
                logger.LogInformation("POST User-{0} send EditRole data",
                   User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                var role = mapper.Map<AccountRoleModel>(updateRole);

                var result = await roleService.UpdateRoleAsync(role);

                if (result.Succeeded)
                {
                    logger.LogInformation("POST User-{0} edited role",
                       User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                    return Ok();
                }

                logger.LogInformation("POST Errors occurred during create role");

                return BadRequest();
            }

            /// <summary>
            /// Get roles
            /// </summary>
            /// <remarks>
            /// Sample request:
            /// POST /Roles
            /// </remarks>
            /// <response code="200">Success</response>
            /// <response code="400">If invalid request</response>
            /// <response code="403">If the user doesn't have rights</response>
            [Authorize(Roles = "Admin")]
            [HttpDelete]
            public async Task<IActionResult> GetAllRolseAsync()
            {
                var roles = await roleService.GetAllRolesAsync();

                var model = new GetRolesResponse();

                foreach (var role in roles)
                {
                    model.Roles.Add(mapper.Map<RoleViewModel>(role));
                }

                logger.LogInformation("GET RoleList page responsed for user-{0}",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return Ok();
            }
        }
    }
}

