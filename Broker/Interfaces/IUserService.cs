using BrokerApi.DTOs;
using BrokerApi.Models;

namespace BrokerApi.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(UserDto user, byte[] passwordHash, byte[] passwordSalt);
        public bool IsUserAlreadyRegistered(UserDto user);
        public Task<User?> FindRegisteredUser(LoggedUserDto loggedUser);
        public Task<User?> VerifyUserWithToken(string token);
        public void ConfirmUserRegistration(User user);
    }
}
