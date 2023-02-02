﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task CreateUser(UserDto user, byte[] passwordHash, byte[] passwordSalt)
        {
            await _userRepository.CreateUser(user, passwordHash, passwordSalt);
        }

        public bool IsUserAlreadyRegistered(UserDto user)
        {
            bool exists = _userRepository.IsUserAlreadyRegistered(user);
            return exists;
        }

        public async Task<User?> FindLoggedInUser(LoggedUserDto loggedUser)
        {
            return await _userRepository.FindLoggedInUser(loggedUser);
        }
    }
}