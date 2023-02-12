using BrokerApi.DTOs;
using BrokerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrokerApi.Interfaces
{
    public interface IAuthService
    {
        public Task<ResultResponse<string>> RegisterUser(UserDto user);
    }
}
