using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BrokerApi.Interfaces;
using BrokerApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BrokerApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateJwtToken(User userLoggedIn)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenOptions = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: new List<Claim>{
                    new Claim(ClaimTypes.Email, userLoggedIn.EmailAddress),
                    new Claim(ClaimTypes.Name, userLoggedIn.Username)
                },
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signInCredentials,
                notBefore: DateTime.Now
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
