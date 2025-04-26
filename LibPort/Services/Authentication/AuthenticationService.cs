using LibPort.Contexts;
using LibPort.Exceptions;
using LibPort.Services.Jwt;
using Microsoft.EntityFrameworkCore;

namespace LibPort.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly LibraryContext _context;

        public AuthenticationService(ITokenService tokenService, LibraryContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<TokenPackage> HandleLogin(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null) throw new NotFoundException("User not found");
            return new TokenPackage
            {
                AccessToken = _tokenService.CreateAccessToken(user),
                RefreshToken = _tokenService.CreateRefreshToken(user),
            };
        }

        public TokenPackage HandleRefreshToken(string refreshToken)
        {
            var claimsprincipal = _tokenService.GetClaimsPrincipal(refreshToken);
            if (claimsprincipal == null) throw new TokenInvalidException();
            var newAccessToken = _tokenService.RenewAccessToken(claimsprincipal.Claims);
            return new TokenPackage
            {
                AccessToken = newAccessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
