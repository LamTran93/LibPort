using LibPort.Models;
using System.Security.Claims;

namespace LibPort.Services.Jwt
{
    public interface ITokenService
    {
        public string GenerateToken(IEnumerable<Claim> claims, TimeSpan timeToExpire);
        public string CreateAccessToken(User user);
        public string CreateRefreshToken(User user);
        public ClaimsPrincipal? GetClaimsPrincipal(string token);
    }
}
