using LibPort.Contexts;
using LibPort.Exceptions;
using LibPort.Models;
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
                .FirstOrDefaultAsync(u => u.Username.Equals(username) && u.Password.Equals(password));
            if (user == null) throw new NotFoundException($"Can't find user with username & password combination");
            return new TokenPackage
            {
                AccessToken = _tokenService.CreateAccessToken(user),
                RefreshToken = _tokenService.CreateRefreshToken(user),
            };
        }

        public async Task<User> HandleRegister(User user)
        {
            if (user.Id != Guid.Empty) user.Id = Guid.NewGuid();
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public TokenPackage HandleRefreshToken(string refreshToken)
        {
            var claimsprincipal = _tokenService.GetClaimsPrincipal(refreshToken);
            if (claimsprincipal == null) throw new TokenInvalidException("Can't read JWT token");
            var newAccessToken = _tokenService.RenewAccessToken(claimsprincipal.Claims);
            return new TokenPackage
            {
                AccessToken = newAccessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
