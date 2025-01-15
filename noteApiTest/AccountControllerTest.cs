using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using noteapi.Controllers;
using noteapi.Dto;
using noteapi.Services;
using System.Threading.Tasks;
using Xunit;

namespace noteApiTest
{
    public class AccountControllerTest
    {
        private readonly AccountController _accountController;
        private readonly Mock<IUserService> _userService;
        private readonly Mock<ILogger<AccountController>> _logger;

        public AccountControllerTest()
        {
            _userService = new Mock<IUserService>();
            _logger = new Mock<ILogger<AccountController>>();
            _accountController = new AccountController(_userService.Object, _logger.Object);
        }

        [Fact]
        public async Task TestLogin_Success()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var token = "testtoken";

            _userService.Setup(s => s.Authenticate(username, password)).ReturnsAsync(token);

            // Act
            var result = await _accountController.Login(username, password);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(token, actionResult.Value);
        }

        [Fact]
        public async Task TestLogin_Failure()
        {
            // Arrange
            var username = "testuser";
            var password = "wrongpassword";

            _userService.Setup(s => s.Authenticate(username, password)).ReturnsAsync((string)null);

            // Act
            var result = await _accountController.Login(username, password);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task TestLogin_BadRequest()
        {
            // Act
            var result = await _accountController.Login(null, null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestRegister_Success()
        {
            // Arrange
            var userRequest = new UserRequest
            {
                UserName = "testuser",
                Password = "testpassword"
            };

            _userService.Setup(s => s.Register(userRequest)).Returns(Task.CompletedTask);

            // Act
            var result = await _accountController.Post(userRequest);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, actionResult.StatusCode);
            Assert.Equal("Create Successfully", actionResult.Value);
        }

        [Fact]
        public async Task TestRegister_BadRequest()
        {
            // Arrange
            _accountController.ModelState.AddModelError("UserName", "Required");

            var userRequest = new UserRequest
            {
                UserName = "",
                Password = "testpassword"
            };

            // Act
            var result = await _accountController.Post(userRequest);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(actionResult.Value);
        }
    }
}
