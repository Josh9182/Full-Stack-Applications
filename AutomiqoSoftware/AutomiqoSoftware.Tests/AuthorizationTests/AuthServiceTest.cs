using Xunit;
using Moq;
using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;
using System.Text;
using AutomiqoSoftware.Sessions;
using AutomiqoSoftware.Models.Users;
using Microsoft.EntityFrameworkCore;
using AutomiqoSoftware.DTOs.ServiceDTOs.AuthorizationDTO;
using AutomiqoSoftware.Interfaces.AuthorizationInterfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.OpenApi.Validations;
using System.Threading.Tasks;

namespace AutomiqoSoftware.AuthTests;

[Trait("Category", "AuthServiceTest")]
public class AuthServiceTests {
    [Trait("Category", "RegisterUserVI")]
    [Fact]
    public async Task RegisterUser_valid_input() {

        var userDto = new UserDto { // DTO mock
            UserEmail = "example@email.com",
            Username = "exampleName",
            Password = "blahblah123"
        };

        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "AuthTestDb")
        .Options;

        using var context = new AppDbContext(options); /* Create an empty AppDbContext mock, 
                                                          disposed after use. 
                                                       */

        var mockCryptoService = new Mock<ICryptoService>(); // Response wrapper mock

        var saltBytes = RandomNumberGenerator.GetBytes(16);
        var saltBase64 = Convert.ToBase64String(saltBytes);

        mockCryptoService.Setup(m => m.GenerateSalt())
            .Returns(CryptoResponse<string>.Success(saltBase64));

        mockCryptoService.Setup(m => m.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
            .Returns(CryptoResponse<string>.Success("hashpass123"));

        var tester = new AuthService(context, mockCryptoService.Object); /* Instance container with 
                                                                            mocked dependencies 
                                                                            via object-value pair. i.e
                                                                            ICryptoService (object)
                                                                            _cryptoService (value).
                                                                         */

        var result = await tester.RegisterUser(userDto);

        Assert.True(result.IsSuccess);
        Assert.Null(result.ErrorMessage);
        Assert.NotNull(result.Data);

    }

    [Trait("Category", "UserLoginVI")]
    [Fact]
    public async Task UserLogin_valid_input() {
        var userDto = new UserDto { // DTO mock
            UserEmail = "example@email.com",
            Username = "exampleName",
            Password = "blahblah123"
        };

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "AuthTestDb")
            .Options;

        var saltBytes = RandomNumberGenerator.GetBytes(16);
        string saltBase64 = Convert.ToBase64String(saltBytes);

        using (var context = new AppDbContext(options)) {
            context.User.Add(new User {
                UserID = Guid.NewGuid(),
                UserEmail = "example@email.com",
                Username = "exampleName",
                HashedPass = "hashpass123",
                Salt = saltBase64
            });
            await context.SaveChangesAsync();

            var mockCryptoService = new Mock<ICryptoService>();

            mockCryptoService.Setup(n => n.HashPassword("blahblah123", saltBytes))
                .Returns(CryptoResponse<string>.Success("hashpass123"));

            var tester = new AuthService(context, mockCryptoService.Object);

            var results = await tester.UserLogin(userDto);

            Assert.True(results);
        }
    }

    [Trait("Category", "UserLoginII")]
    [Fact]
    public async Task UserLogin_invalid_input() {
        var userDto = new UserDto {
            UserEmail = "example@email.com",
            Username = "exampleName",
            Password = "blahblah123"
        };

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "testDB")
            .Options;

        var saltBytes = RandomNumberGenerator.GetBytes(16);
        string saltBase64 = Convert.ToBase64String(saltBytes);

        using (var context = new AppDbContext(options)) {
            context.User.Add(new User {
                UserID = Guid.NewGuid(),
                UserEmail = "example@email.com",
                Username = "exampleName",
                HashedPass = "hashpass123",
                Salt = saltBase64
            });
            await context.SaveChangesAsync();

            var mockCryptoService = new Mock<ICryptoService>();

            mockCryptoService.Setup(n => n.HashPassword("blahblah123", saltBytes))
                .Returns(CryptoResponse<string>.Success("invalid_hash123"));

            var tester = new AuthService(context, mockCryptoService.Object);

            var results = await tester.UserLogin(userDto);

            Assert.False(results);
        }
    }
}