using BrokerApi.DTOs;
using BrokerApi.Models;

namespace BrokerApi.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDto user, byte[] passwordHash, byte[] passwordSalt);
        public bool IsUserAlreadyRegistered(UserDto user);
        public Task<User?> FindLoggedInUser(LoggedUserDto loggedUser);
    }
}
