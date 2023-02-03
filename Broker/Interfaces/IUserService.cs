using BrokerApi.DTOs;
using BrokerApi.Models;

namespace BrokerApi.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDto user, byte[] passwordHash, byte[] passwordSalt);
        public bool IsUserAlreadyRegistered(UserDto user);
        public Task<User?> FindRegisteredUser(LoggedUserDto loggedUser);
        public Task<User?> VerifyUserWithToken(string token);
    }
}
