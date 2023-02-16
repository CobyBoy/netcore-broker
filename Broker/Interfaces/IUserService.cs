using BrokerApi.DTOs;
using BrokerApi.Models;

namespace BrokerApi.Interfaces
{
    public interface IUserService
    {
        public Task<bool> HasUserConfirmedEmail(LoggedUserDto userToBeLoggedIn);
        public Task<ApiResponse<string>> VerifyUserByToken(string verificationToken);
    }
}
