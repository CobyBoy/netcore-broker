﻿using BrokerApi.DTOs;
using BrokerApi.Models;

namespace BrokerApi.Interfaces
{
    public interface IUserRepository
    {
        public bool IsUserAlreadyRegistered(UserDto user);
        public Task CreateUser(UserDto user, byte[] passwordHash, byte[] passwordSalt);
        public Task<User?> FindLoggedInUser(LoggedUserDto loggedUser);
    }
}