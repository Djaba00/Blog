using AutoMapper;
using Blog.API.Models.Requests.SignIn;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.API.Models.ViewModels;
using Blog.API.Models.Responses.Account;
using Blog.BLL.Exceptions;
using Blog.API.Models.Requests.Account;

namespace Blog.API.Controllers.Account
{
    [Produces("application/json")]
    [Route("api/[controller]")]
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

        /// <summary>
        /// User's login
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Login
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="401">Login failed</response>
        [Route("Login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync(LoginRequest login)
        {
            logger.LogInformation("POST User send Login data");

            var user = mapper.Map<UserAccountModel>(login);

            var result = await signInService.LoginAsync(user);

            if (result.Succeeded)
            {
                logger.LogInformation("{0} POST Login succesfull");

                return Ok();
            }
            else
            {
                logger.LogInformation("{0} POST Errors occurred during login");

                return Unauthorized();
            }
        }

        /// <summary>
        /// User Logout
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Logout
        /// </remarks>
        /// <response code="200">Success</response>
        [Authorize]
        [Route("Logout")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LogoutAsync()
        {
            await signInService.LogoutAsync();

            logger.LogInformation("POST Logout user-{0} successfull",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return Ok();
        }

        /// <summary>
        /// Accounts list
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Accounts
        /// </remarks>
        /// <returns>Returns GetAccountsResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [Route("Accounts")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccountsListAsync()
        {
            var users = await accountService.GetAllAcсountsAsync();

            var model = new GetAccountsResponse();

            foreach (var user in users)
            {
                model.Accounts.Add(mapper.Map<AccountViewModel>(user));
            }

            logger.LogInformation("GET Accounts page responsed");

            return Ok(model);
        }

        /// <summary>
        /// Accounts by role
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Role/{roleName:string}
        /// </remarks>
        /// <returns>Returns GetAccountsResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [Route("Role/{roleName:string}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccountListByRoleAsync(string roleName)
        {
            var accounts = await accountService.GetAccountsByRoleAsync(roleName);

            var role = await roleService.GetRoleByNameAsync(roleName);

            var model = new GetAccountsByRoleResponse();

            var roleModel = mapper.Map<RoleViewModel>(role);

            model.Role = roleModel;

            foreach (var user in accounts)
            {
                model.Accounts.Add(mapper.Map<AccountViewModel>(user));
            }

            logger.LogInformation("GET Accounts page by {0} role responsed",
                roleName);

            return Ok(model);
        }

        /// <summary>
        /// Account by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Account/{id:int}
        /// </remarks>
        /// <returns>Returns GetAccountsResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [Route("Account/{id:int}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccountAsync(string id)
        {
            logger.LogInformation("GET user-{0} requested User-{1} page",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                id);

            var user = await accountService.GetAccountByIdAsync(id);

            var model = mapper.Map<GetAccountResponse>(user);

            logger.LogInformation("GET User-{0} page responsed for user-{1}",
                id,
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return Ok(model);
        }

        /// <summary>
        /// Edit account
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user doesn't have rights</response>
        /// <response code="400">If invalid request</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserAsync(EditAccountRequest updateUser)
        {
            try
            {
                logger.LogInformation("POST user-{0} send EditAccount data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                var user = mapper.Map<UserAccountModel>(updateUser);

                await accountService.UpdateAccountAsync(User, user);

                if (updateUser.NewPassword != null && updateUser.OldPassword != null)
                {
                    var result = await accountService.ChangePasswordAsync(user, updateUser.OldPassword, updateUser.NewPassword);
                }

                logger.LogInformation("POST user-{0} account update successfull",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return Ok();
            }
            catch (ForbiddenException )
            {
                return Forbid();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete account
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user doesn't have rights</response>
        /// <response code="400">If invalid request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            try
            {
                logger.LogInformation("POST user-{0} send delete account data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                var accountModel = mapper.Map<UserAccountModel>(id);

                await accountService.DeleteAccountAsync(User, id);

                logger.LogInformation("POST user-{0} delete user-{1}",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    id);

                return Ok();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

