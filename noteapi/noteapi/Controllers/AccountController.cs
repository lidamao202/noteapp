using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using noteapi.Dto;
using noteapi.Models;
using noteapi.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace noteapi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _user;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IUserRepository user, IConfiguration configuration, ILogger<AccountController> logger)
        {
            _user = user;
            _configuration = configuration;
            _logger = logger;
        }
        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username,string password)
        {
            try
            {
                if (username == null || password == null)
                {
                    return BadRequest();
                }
                var user = await _user.Get(username);
                if (user.Password == password)
                {
                    string token = GenerateJSONWebToken(user);
                    return Ok(token);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"login");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] UserRequest userRequest)
        {
            try
            {
                //if (userRequest == null)
                //{
                //    return BadRequest();
                //}
                _user.Save(userRequest);

                return StatusCode(201, "Create Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "login");
                return StatusCode(500, ex.Message);
            }
        }


        private string GenerateJSONWebToken(User user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim("Role","User"),
                new Claim("UserId",user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = issuer,
                Audience = audience,
                //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

    }
}
