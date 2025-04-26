using LibPort.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibPort.Services.Jwt
{
    public class TokenService : ITokenService
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly SymmetricSecurityKey _key;
        private readonly TimeSpan _accessTokenExpireTime = TimeSpan.FromMinutes(30);
        private readonly TimeSpan _refreshTokenExpireTime = TimeSpan.FromDays(12);

        public TokenService(IConfiguration configuration)
        {
            _issuer = configuration["Jwt:Issuer"]!;
            _audience = configuration["Jwt:Audience"]!;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!));
        }

        public string GenerateToken(IEnumerable<Claim> claims, TimeSpan timeToExpire)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var issuedAtClaim = new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
            var withIatClaims = claims.Append(issuedAtClaim);
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: withIatClaims,
                expires: DateTime.Now.Add(timeToExpire),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateAccessToken(User user)
        {
            var tokenTypeClaim = new Claim("type", "access_token");
            var userTypeClaim = new Claim("user_type", user.UserType.ToString());
            return GenerateToken([tokenTypeClaim, userTypeClaim], _accessTokenExpireTime);
        }

        public string RenewAccessToken(IEnumerable<Claim> claims)
        {
            return GenerateToken(claims
                .Where(c => c.Type != "iat" && c.Type != "aud" && c.Type != "type")
                .Append(new Claim("type", "access_token")), _accessTokenExpireTime);
        }

        public string CreateRefreshToken(User user)
        {
            var tokenTypeClaim = new Claim("type", "refresh_token");
            var userTypeClaim = new Claim("user_type", user.UserType.ToString());
            return GenerateToken([tokenTypeClaim, userTypeClaim], _refreshTokenExpireTime);
        }

        public ClaimsPrincipal? GetClaimsPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidateLifetime = true
            };

            try
            {
                return tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
