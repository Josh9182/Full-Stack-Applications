using Xunit;
using Moq;
using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;
using System.Text;
using AutomiqoSoftware.Crypto;

namespace AutomiqoSoftware.AuthTests;

[Trait("Category", "FullCryptoServiceTest")]
public class CryptoServiceTests {
    [Trait("Category", "CryptoServiceVI")] // GenerateSalt test for method validity.
    [Fact]
    public void CryptoService_valid_input() {
        var tester = new CryptoService();

        CryptoResponse<string> result = tester.GenerateSalt();

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Null(result.ErrorMessage);
    }

    [Trait("Category", "HashPasswordVI")]
    [Fact]
    public void HashPassword_valid_input() { // HashPassword test for method validity.
        string password = "test_password123";
        byte[] bytes = new byte[16]; ;

        var tester = new CryptoService();

        CryptoResponse<string> result = tester.HashPassword(password, bytes);

        Assert.True(result.IsSuccess);
        
    }

}