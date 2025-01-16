using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noteapi.Dto;
using noteapi.Services;
using System.Threading.Tasks;

namespace noteapi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            _logger.LogInformation("Login method called with username: {Username}", username);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                _logger.LogWarning("Username or password is null or empty.");
                return BadRequest("Username and password are required.");
            }
            var token = await _userService.Authenticate(username, password);
            if (token == null)
            {
                _logger.LogWarning("Authentication failed for username: {Username}", username);
                return Unauthorized();
            }

            _logger.LogInformation("User {Username} authenticated successfully.", username);
            return Ok(token);
        }

        [HttpGet]
        [Route("getAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll method called.");

            var users = await _userService.GetAll();

            _logger.LogInformation("GetAll method completed successfully.");
            return Ok(users);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] UserRequest userRequest)
        {
            _logger.LogInformation("Register method called for username: {Username}", userRequest.UserName);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for username: {Username}", userRequest.UserName);
                return BadRequest(ModelState);
            }
            if (await _userService.UserExists(userRequest.UserName))
            {
                _logger.LogWarning("User already exists with username: {Username}", userRequest.UserName);
                return Conflict("Invalid username and password.");
            }

            await _userService.Register(userRequest);
            _logger.LogInformation("User {Username} registered successfully.", userRequest.UserName);
            return StatusCode(201, "Create Successfully");
        }
    }
}
