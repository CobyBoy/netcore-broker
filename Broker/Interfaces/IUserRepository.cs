using BrokerApi.DTOs;
using BrokerApi.Models;

namespace BrokerApi.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> HasUserConfirmedEmail(LoggedUserDto userToBeLoggedIn);
        public Task<ApiResponse<string>> VerifyUserByToken(string verificationToken);
    }
}
