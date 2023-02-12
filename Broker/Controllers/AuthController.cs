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
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;


        public AuthController(IUserService userService, IHashingService hashingService, ITokenService tokenService, IAuthService authService)
        {
            _userService = userService;
            _hashingService = hashingService;
            _tokenService = tokenService;
            _authService = authService;
        }

        // POST api/<AuthController>/register
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            var response = _authService.RegisterUser(user);
            return CreatedAtAction(nameof(Register), response.Result);
            
            //bool userExists = _userService.IsUserAlreadyRegistered(user);
            //if (!userExists)
            //{
            //    _hashingService.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            //    var newUser = await _userService.CreateUser(user, passwordHash, passwordSalt);
            //    //Send email with the verification token to the user
            //    _emailService.SendEmail(newUser.EmailAddress, newUser.VerificationToken);
            //    return CreatedAtAction(nameof(Register), new { id = newUser.Id, message = "Registration successful. Please check your email to verify your user" }, new { id = newUser.Id });
            //}
            //else return BadRequest(new { message = "User already registered" });
            //return CreatedAtAction(_authService);

        }

        // POST api/<AuthController>/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoggedUserDto loggedUser)
        {
            if (loggedUser == null) return BadRequest(new { message = "Please fill all fields" });
            var userLoggedIn = await _userService.FindRegisteredUser(loggedUser);
            if (userLoggedIn == null) { return NotFound(new { message = "Wrong user or password" }); }
            if (!userLoggedIn.isUserVerified) { return BadRequest(new { message = "You need to verify your email first" }); }
            if (!_hashingService.VerifyPasswordHash(loggedUser.Password, userLoggedIn.PasswordHash, userLoggedIn.PasswordSalt))
            {
                return BadRequest(new { message = "Invalid credentials" });
            }
            var jwtToken = _tokenService.CreateJwtToken(userLoggedIn);
            var authResponse = new AuthenticationResponse { Access_Token = jwtToken, Expires_In = DateTime.Now.AddDays(1), Refresh_Token = "sasa", Token_Type = "Bearer" };
            return Ok(authResponse);


        }

        [HttpPost("confirm-email/{token}")]
        public async Task<IActionResult> ConfirmaUserRegistration(string token)
        {
            var user = await _userService.VerifyUserWithToken(token);
            if (user == null) return NotFound("Invalid token");
            _userService.ConfirmUserRegistration(user);
            return Ok(new { message = "User verified" });
        }

        [HttpPost("test")]
        public async Task<IActionResult> Testmail([FromBody] GoogleDto credential)
        {
            //_emailService.SendEmail();
            return Ok(new { credential });
        }

    }
}
