using Microsoft.AspNetCore.Mvc;
using BrokerApi.DTOs;
using BrokerApi.Interfaces;
using BrokerApi.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace BrokerApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<string>> VerifyUserByToken(string verificationToken)
        {
            return await _userRepository.VerifyUserByToken(verificationToken);
        }

        public async Task<bool> HasUserConfirmedEmail(LoggedUserDto userToBeLoggedIn)
        {
            return await _userRepository.HasUserConfirmedEmail(userToBeLoggedIn);
        }
    }
}
