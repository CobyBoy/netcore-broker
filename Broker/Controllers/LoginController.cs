//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using BrokerApi.Models;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using System.Text.RegularExpressions;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace BrokerApi.Controllers
//{
//    [Route("api/auth/[controller]")]
//    [ApiController]
//    public class LoginController : ControllerBase
//    {
//        private readonly IConfiguration _configuration;

//        public LoginController(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }
//        // GET: api/<LoginController>
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/<LoginController>/5
//        [HttpGet("{id}")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST api/<LoginController>

//        [HttpPost]
//        public async Task<ActionResult> Login([FromBody] Credentials credentials)
//        {
//            if (credentials == null)
//            {
//                return BadRequest("Email or password is wrong");
//            }
//            if (credentials.Email == "aboutacoby@gmail.com" && credentials.Password == "1234")
//            {
//                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
//                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
//                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
//                var tokenOptions = new JwtSecurityToken(
//                    issuer: jwt.Issuer,
//                    audience: jwt.Audience,
//                    claims: new List<Claim>(),
//                    expires: DateTime.Now.AddMinutes(5),
//                    signingCredentials: signInCredentials,
//                    notBefore: DateTime.Now
//                    );
//                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
//                return Ok(new AuthenticationResponse
//                {
//                    Token = tokenString
//                });
//            }

//            return Unauthorized();
//        }

//        // PUT api/<LoginController>/5
//        [HttpPut("{ id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<LoginController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
