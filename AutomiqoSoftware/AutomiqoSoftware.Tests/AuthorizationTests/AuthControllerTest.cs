using AutomiqoSoftware.Models.Users;
using Microsoft.AspNetCore.Mvc;
using AutomiqoSoftware.Controllers;
using AutomiqoSoftware.DTOs.AuthorizationDTO.Users;
using AutomiqoSoftware.Interfaces.AuthorizationInterfaces;
using AutomiqoSoftware.ServiceResponses.AuthorizationWrappers;
using Moq;
using Xunit;

namespace AutomiqoSoftware.AuthorizationTests;

[Trait("Category", "AuthControllerTest")]
public class AuthControllerTests {
    [Trait("Category", "AuthControllerRV")]
    [Fact]
    public async Task AuthController_register_valid() {
        var userDto = new UserDto { // Mock DTO
            UserEmail = "test@example.com",
            Username = "testname",
            Password = "testpass123"
        };

        var mockAuthService = new Mock<IAuthService>(); // AuthService mock
        mockAuthService.Setup(n => n.RegisterUser(userDto))
            .ReturnsAsync(CryptoResponse<string>.Success("Success!")); /* Run RegisterUser, 
                                                                          if success == 
                                                                          Success (Ok())
                                                                       */

        var tester = new AuthController(mockAuthService.Object);

        var results = await tester.Register(userDto);

        var okResult = Assert.IsType<OkObjectResult>(results);
        var value = Assert.IsType<CryptoResponse<string>>(okResult.Value);
        Assert.Equal("Success!", value.Data); // Value SHOULD be an OkObjectResult of CryptoResponse data
    }

    [Trait("Category", "AuthControllerRI")]
    [Fact]
    public async Task AuthController_register_invalid() {
        var userDto = new UserDto {
            UserEmail = "test@example.com",
            Username = "testname",
            Password = "testpass123"
        };

        var mockAuthService = new Mock<IAuthService>();
        mockAuthService.Setup(n => n.RegisterUser(userDto))
            .ReturnsAsync(CryptoResponse<string>.Failure("Failure!")); /* Run RegisterUser, 
                                                                          if success == 
                                                                          Failure (BadRequest())
                                                                       */

        var tester = new AuthController(mockAuthService.Object);

        var results = await tester.Register(userDto);

        var badResult = Assert.IsType<BadRequestObjectResult>(results);
        Assert.Equal("Failure!", badResult.Value);
    }

    [Trait("Category", "AuthControllerLV")]
    [Fact]
    public async Task AuthController_login_valid() {
        var userDto = new UserDto { // Mock DTO
            UserEmail = "test@example.com",
            Username = "testname",
            Password = "testpass123"
        };

        var mockAuthService = new Mock<IAuthService>(); // AuthService mock
        mockAuthService.Setup(n => n.UserLogin(userDto))
            .ReturnsAsync(true);

        var tester = new AuthController(mockAuthService.Object);

        var results = await tester.Login(userDto);

        var okResult = Assert.IsType<OkObjectResult>(results);
        Assert.Equal(true, okResult.Value);
    }

    [Trait("Category", "AuthControllerLI")]
    [Fact]
    public async Task AuthController_login_invalid() {
        var userDto = new UserDto { // Mock DTO
            UserEmail = "test@example.com",
            Username = "testname",
            Password = "testpass123"
        };

        var mockAuthService = new Mock<IAuthService>(); // AuthService mock
        mockAuthService.Setup(n => n.UserLogin(userDto))
            .ReturnsAsync(false);

        var tester = new AuthController(mockAuthService.Object);

        var results = await tester.Login(userDto);

        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(results);
        Assert.Equal("The request lacks valid authentication credentials for the " +
            "requested resource.", unauthorizedResult.Value);
    }
}