using System.Security.Claims;
using LibPort.Models;
using LibPort.Services.Jwt;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UnitTest.ServiceTests
{
    [TestFixture]
    public class TokenServiceTests
    {
        private TokenService _tokenService;

        [SetUp]
        public void Setup()
        {
            // Mock IConfiguration
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            configurationMock.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");
            configurationMock.Setup(c => c["Jwt:SecretKey"]).Returns("TestSecretKey1234567890");

            _tokenService = new TokenService(configurationMock.Object);
        }

        [Test]
        public void GenerateToken_ShouldReturnValidJwtToken()
        {
            // Arrange
            var claims = new[]
            {
                new Claim("type", "access_token"),
                new Claim("user_id", Guid.NewGuid().ToString())
            };
            var timeToExpire = TimeSpan.FromMinutes(30);

            // Act
            var token = _tokenService.GenerateToken(claims, timeToExpire);

            // Assert
            Assert.IsNotNull(token);
            var handler = new JwtSecurityTokenHandler();
            Assert.IsTrue(handler.CanReadToken(token));
        }

        [Test]
        public void CreateAccessToken_ShouldReturnValidAccessToken()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                UserType = UserType.NormalUser
            };

            // Act
            var token = _tokenService.CreateAccessToken(user);

            // Assert
            Assert.IsNotNull(token);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "type" && c.Value == "access_token"));
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "user_type" && c.Value == UserType.NormalUser.ToString()));
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "user_id" && c.Value == user.Id.ToString()));
        }

        [Test]
        public void RenewAccessToken_ShouldReturnNewAccessToken()
        {
            // Arrange
            var claims = new[]
            {
                new Claim("type", "refresh_token"),
                new Claim("user_id", Guid.NewGuid().ToString()),
                new Claim("user_type", UserType.NormalUser.ToString())
            };

            // Act
            var token = _tokenService.RenewAccessToken(claims);

            // Assert
            Assert.IsNotNull(token);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "type" && c.Value == "access_token"));
            Assert.IsTrue(long.Parse(jwtToken.Claims.First(c => c.Type == "exp").Value) > DateTimeOffset.Now.ToUnixTimeSeconds()); 
        }

        [Test]
        public void CreateRefreshToken_ShouldReturnValidRefreshToken()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                UserType = UserType.NormalUser
            };

            // Act
            var token = _tokenService.CreateRefreshToken(user);

            // Assert
            Assert.IsNotNull(token);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "type" && c.Value == "refresh_token"));
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "user_type" && c.Value == UserType.NormalUser.ToString()));
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "user_id" && c.Value == user.Id.ToString()));
        }

        [Test]
        public void GetClaimsPrincipal_ShouldReturnValidClaimsPrincipal()
        {
            // Arrange
            var claims = new[]
            {
                new Claim("type", "access_token"),
                new Claim("user_id", Guid.NewGuid().ToString())
            };
            var token = _tokenService.GenerateToken(claims, TimeSpan.FromMinutes(30));

            // Act
            var claimsPrincipal = _tokenService.GetClaimsPrincipal(token);

            // Assert
            Assert.IsNotNull(claimsPrincipal);
            Assert.IsTrue(claimsPrincipal.HasClaim(c => c.Type == "type" && c.Value == "access_token"));
        }

        [Test]
        public void GetClaimsPrincipal_ShouldReturnNullForInvalidToken()
        {
            // Arrange
            var invalidToken = "InvalidToken";

            // Act
            var claimsPrincipal = _tokenService.GetClaimsPrincipal(invalidToken);

            // Assert
            Assert.IsNull(claimsPrincipal);
        }
    }
}
