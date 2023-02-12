using BrokerApi.DTOs;
using BrokerApi.Interfaces;
using BrokerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace BrokerApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IHashingService _hashingService;
        private readonly IEmailService _emailService;
        public AuthService(IUserService userService, IHashingService hashingService, IEmailService emailService)
        {
            _userService = userService;
            _hashingService = hashingService;
            _emailService = emailService;
        }
        public async Task<ResultResponse<string>> RegisterUser(UserDto user)
        {
            ResultResponse<string> response = new ResultResponse<string>();
            bool userExists = _userService.IsUserAlreadyRegistered(user);
            if (!userExists)
            {
                _hashingService.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                User newUser = await _userService.CreateUser(user, passwordHash, passwordSalt);
                //Send email with the verification token to the user
                _emailService.SendEmail(newUser.EmailAddress, newUser.VerificationToken);
                response.Data = newUser.Id.ToString();
                response.Message = "Registration successful. Please check your email to verify your user";
            }
            else response.Message = "User already registered";
            return response;
        }
    }
}
