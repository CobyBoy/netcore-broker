﻿using Microsoft.AspNetCore.Mvc;
using BrokerApi.DTOs;
using BrokerApi.Interfaces;

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


        public AuthController(IUserService userService, IHashingService hashingService, ITokenService tokenService)
        {
            _userService = userService;
            _hashingService = hashingService;
            _tokenService = tokenService;
        }

        // POST api/<AuthController>/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            if (user == null) return BadRequest("Please fill all fields");
            bool userExists = _userService.IsUserAlreadyRegistered(user);
            if (!userExists)
            {
                _hashingService.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                await _userService.CreateUser(user, passwordHash, passwordSalt);
                return Ok(new { message = "User successfully created" });
            }
            else return BadRequest(new { message = "User already registered" });

        }

        // POST api/<AuthController>/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoggedUserDto loggedUser)
        {
            if (loggedUser == null) return BadRequest(new { message = "Please fill all fields" });
            var userLoggedIn = await _userService.FindLoggedInUser(loggedUser);
            if (userLoggedIn == null) { return BadRequest(new { message = "Wrong user or password" }); }
            if (!userLoggedIn.isUserVerified) { return BadRequest(new { message = "You need to verify your email first" }); }
            if (!_hashingService.VerifyPasswordHash(loggedUser.Password, userLoggedIn.PasswordHash, userLoggedIn.PasswordSalt))
            {
                return BadRequest(new { message = "Invalid credentials" });
            }
            var jwtToken = _tokenService.CreateJwtToken(userLoggedIn);
            return Ok(new { token = jwtToken, message = "Log in successfull" });


        }

    }
}