using Microsoft.AspNetCore.Mvc;
using BrokerApi.DTOs;
using BrokerApi.Interfaces;
using BrokerApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrokerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHashingService _hashingService;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;


        public AuthController(IUserService userService, IHashingService hashingService, IAuthService authService)
        {
            _userService = userService;
            _hashingService = hashingService;
            _authService = authService;
        }

        // POST api/<AuthController>/register
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<string>>> Register([FromBody] RegisterUserDto userToBeRegistered)
        {
            var response = await _authService.RegisterUser(userToBeRegistered);
            return CreatedAtAction(nameof(Register), response);
        }

        // POST api/<AuthController>/login
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] LoggedUserDto userToBeLoggedIn)
        {
            if (userToBeLoggedIn == null) return BadRequest(new ApiResponse<string> { Message = "Please fill all fields" });
            if (!await _authService.ValidateUser(userToBeLoggedIn)) { return Unauthorized(new ApiResponse<string> { Message = "Wrong user or password" }); }
            if (await _userService.HasUserConfirmedEmail(userToBeLoggedIn)) return Unauthorized(new ApiResponse<string> { Message = "You need to verify your email first" });
            return Ok(await _authService.LogInUser(userToBeLoggedIn));
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Logout()
            {
            _authService.LogOut();
            return NoContent();
            }

        [HttpPost("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<string>>> ConfirmUserRegistration([FromQuery] string verificationToken)
        {
            return Ok(await _userService.VerifyUserByToken(verificationToken));
        }

        [HttpPost("get-new-verification-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNewConfirmationToken([FromBody] string email)
        {
           return Ok(await _authService.GetNewVerificationToken(email));
        }

        [HttpPost("test")]
        public async Task<IActionResult> Testmail([FromBody] GoogleDto credential)
        {
            //_emailService.SendEmail();
            return Ok(new { credential });
        }

    }
}
