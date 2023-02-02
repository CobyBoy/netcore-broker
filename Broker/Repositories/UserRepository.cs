﻿using Microsoft.EntityFrameworkCore;
using BrokerApi.Connection;
using BrokerApi.DTOs;
using BrokerApi.Interfaces;
using BrokerApi.Models;

namespace BrokerApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDataContext _context;
        private readonly IHashingService _hashingService;
        public UserRepository(UserDataContext context, IHashingService hashingService)
        {
            _context = context;
            _hashingService = hashingService;
        }

        public bool IsUserAlreadyRegistered(UserDto user)
        {
            var exists = _context.Users.Any(u => u.Username == user.Username || u.EmailAddress == user.Email);
            return exists;
        }

        public async Task CreateUser(UserDto user, byte[] passwordHash, byte[] passwordSalt)
        {
            var newUser = new User
            {
                Username = user.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = _hashingService.CreateRandomVerificationToken(),
                EmailAddress = user.Email,
                AreaCode = user.AreaCode,
                PhoneNumber = user.PhoneNumber,
                RegisteredAt = DateTime.Now,
                IsActive = true,
                isUserVerified = false,

            };
            _context.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> FindLoggedInUser(LoggedUserDto loggedUser)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == loggedUser.Email);
        }
    }
}
