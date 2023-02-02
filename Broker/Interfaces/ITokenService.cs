using BrokerApi.Models;

namespace BrokerApi.Interfaces
{
    public interface ITokenService
    {
        public string CreateJwtToken(User userLoggedIn);
    }
}
