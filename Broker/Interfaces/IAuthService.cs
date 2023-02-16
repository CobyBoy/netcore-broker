using BrokerApi.DTOs;
using BrokerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrokerApi.Interfaces
{
    public interface IAuthService
    {
        public Task<ApiResponse<string>> RegisterUser(RegisterUserDto userToBeRegistered);
        public Task<bool> ValidateUser(LoggedUserDto userToBeLoggedIn);

        public Task<ApiResponse<AuthenticationResponse>?> LogInUser(LoggedUserDto userToBeLoggedIn);
        public Task<AuthenticationResponse> CreateJwtToken(LoggedUserDto userToBeLoggedIn);
    }
}
