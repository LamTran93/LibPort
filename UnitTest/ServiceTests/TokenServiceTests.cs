using System.Security.Claims;
using LibPort.Services.Jwt;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;

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
            configurationMock.Setup(c => c["Jwt:SecretKey"]).Returns("TestSecretKey123456789011111111111111111111111111111111111");

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

        [Test]
        public void GenerateToken_ShouldIncludeIatClaim()
        {
            // Arrange
            var claims = new[]
            {
                new Claim("type", "access_token")
            };
            var timeToExpire = TimeSpan.FromMinutes(30);

            // Act
            var token = _tokenService.GenerateToken(claims, timeToExpire);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.IsNotNull(jwtToken);
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "iat"));
        }
    }
}
