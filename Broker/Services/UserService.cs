using Microsoft.AspNetCore.Mvc;
using BrokerApi.DTOs;
using BrokerApi.Interfaces;
using BrokerApi.Models;

namespace BrokerApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(UserDto user, byte[] passwordHash, byte[] passwordSalt)
        {
            return await _userRepository.CreateUser(user, passwordHash, passwordSalt);
        }

        public bool IsUserAlreadyRegistered(UserDto user)
        {
            bool exists = _userRepository.IsUserAlreadyRegistered(user);
            return exists;
        }

        public async Task<User?> FindRegisteredUser(LoggedUserDto loggedUser)
        {
            return await _userRepository.FindRegisteredUser(loggedUser);
        }

        public async Task<User?> VerifyUserWithToken(string token)
        {
            return await _userRepository.FindUserByToken(token);
        }

        public void ConfirmUserRegistration(User user)
        {
            if (DateTime.Compare(DateTime.Now, user.RegisteredAt.AddHours(2)) >= 0)
            {
                _userRepository.ConfirmUserRegistration(user);
            }
           
        }
    }
}
