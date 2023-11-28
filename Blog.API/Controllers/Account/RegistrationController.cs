using AutoMapper;
using Blog.API.Models.Requests.SignIn;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers.Account
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        readonly ILogger<RegistrationController> logger;
        readonly IMapper mapper;
        readonly ISignInService signInService;

        public RegistrationController(ILogger<RegistrationController> logger, IMapper mapper, ISignInService signInService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.signInService = signInService;
        }

        /// <summary>
        /// User Registration
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Registration
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Registration failed</response>
        [Route("Registration")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrationAsync(RegistrationRequest registration)
        {
            logger.LogInformation("POST send registration data");

            var account = mapper.Map<UserAccountModel>(registration);

            account.Profile = mapper.Map<UserProfileModel>(registration);

            var result = await signInService.RegistrationAsync(account);

            if (result.Succeeded)
            {
                logger.LogInformation("POST A new user has been registered");

                return Ok();
            }
            else
            {
                logger.LogInformation("POST Errors occurred during registration");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest();
        }
    }
}

