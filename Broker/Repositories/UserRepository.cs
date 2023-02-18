using Microsoft.EntityFrameworkCore;
using BrokerApi.Connection;
using BrokerApi.DTOs;
using BrokerApi.Interfaces;
using BrokerApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace BrokerApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDataContext _context;
        private readonly UserManager<User> _userManager;
        public UserRepository(UserDataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> HasUserConfirmedEmail(LoggedUserDto userToBeLoggedIn)
        {
            return (await _userManager.FindByEmailAsync(userToBeLoggedIn.Email)).EmailConfirmed;
        }

        public async Task<ApiResponse<string>> VerifyUserByToken(string verificationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.VerificationToken == verificationToken);
            if (user == null) { return new ApiResponse<string> { Message = "Invalid token", Success = false }; }
           
            return await ConfirmUserRegistration(user);
        }

        private async Task<ApiResponse<string>> ConfirmUserRegistration(User user)
        {
            if(DateTime.Compare(DateTime.Now, user.RegisteredAt.AddHours(2)) >= 0)
            {
                return new ApiResponse<string> { Message = "Token expired", Success = false };
            }
            else if (user.EmailConfirmed)
            {
                return new ApiResponse<string> { Message = "User already verified", Success = false };
            }
            else
            {
                user.ConfirmedRegistrationAt = DateTime.Now;
                user.EmailConfirmed = true;
                await _context.SaveChangesAsync();
                return new ApiResponse<string> { Message = "User verified" };
            }
        }
    }
}
