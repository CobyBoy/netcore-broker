using AutoMapper;
using BrokerApi.DTOs;
using BrokerApi.Interfaces;
using BrokerApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BrokerApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHashingService _hashingService;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthService(IHashingService hashingService, IEmailService emailService, UserManager<User> userManager, IConfiguration configuration, IMapper mapper,
            SignInManager<User> signInManager)
        {
            _hashingService = hashingService;
            _emailService = emailService;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<ApiResponse<string>> RegisterUser(RegisterUserDto userToBeRegistered)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            User newUser = _mapper.Map<User>(userToBeRegistered);
            newUser.RegisteredAt = DateTime.Now;
            newUser.VerificationToken = _hashingService.CreateRandomVerificationToken();
            var result = await _userManager.CreateAsync(newUser, userToBeRegistered.Password);
            //Send email with the verification token to the user
            if (result.Succeeded)
            {
                _emailService.SendEmail(newUser.Email, newUser.VerificationToken);
                response.Data = newUser.Id.ToString();
                response.Message = "Registration successful. Please verify your email before login";
            }
            else
            {
                response.Message = string.Join(" ", result.Errors.Select(err => err.Description));
            }
            return response;
        }

        public async Task<bool> ValidateUser(LoggedUserDto userToBeLoggedIn)
        {
            User? user = await _userManager.FindByEmailAsync(userToBeLoggedIn.Email);
            return (user != null && await _userManager.CheckPasswordAsync(user, userToBeLoggedIn.Password));
        }

        public async Task<ApiResponse<AuthenticationResponse>?> LogInUser(LoggedUserDto userToBeLoggedIn)
        {
            var user = await _userManager.FindByEmailAsync(userToBeLoggedIn.Email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, userToBeLoggedIn.Password, false, false);
            var response = new ApiResponse<AuthenticationResponse>();
            if (result.Succeeded)
            {
                response.Data = await CreateJwtToken(userToBeLoggedIn);
                response.Message = "Log in successfull";
            }
            else
            {
                response.Message = "Log in failed";
                response.Success = false;
            }
            return response;
        }

        public async Task<ApiResponse<string>> GetNewVerificationToken(string email)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            User? user = await _userManager.FindByEmailAsync(email);
            if(user != null) {
                _emailService.ReSendEmail(email, _hashingService.CreateRandomVerificationToken());
                response.Message = "A new confirmation email has been sent to you.";
            }
            else
            {
                response.Message = "Your email is not registered";
                response.Success = false;
            } 
            return response;
            
        }

        public async void LogOut()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<AuthenticationResponse> CreateJwtToken(LoggedUserDto userToBeLoggedIn)
        {
            List<Claim> claims = await GetClaims(userToBeLoggedIn);
            Jwt jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            SigningCredentials signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken tokenOptions = GetJwtOptions(jwt, claims, signInCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new AuthenticationResponse
            {
                Access_Token = tokenString,
                Expires_In = DateTime.Now.AddDays(1),
                Refresh_Token = "some_refresh_token",
                Token_Type = "Bearer"
            };
        }

        private async Task<List<Claim>> GetClaims(LoggedUserDto userToBeLoggedIn)
        {
            User? user = await _userManager.FindByEmailAsync(userToBeLoggedIn.Email);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        private JwtSecurityToken GetJwtOptions(Jwt jwt, List<Claim> claims, SigningCredentials signInCredentials)
        {
            return new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signInCredentials,
                notBefore: DateTime.Now
                );
        }
    }
}
