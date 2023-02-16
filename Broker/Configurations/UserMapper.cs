using AutoMapper;
using BrokerApi.DTOs;
using BrokerApi.Models;

namespace BrokerApi.Configurations
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
           CreateMap<User,RegisterUserDto>().ReverseMap();
        }
    }
}
