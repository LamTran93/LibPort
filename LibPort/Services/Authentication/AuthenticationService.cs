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

        public async Task<TokenResponse> HandleLogin(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null) throw new NotFoundException("User not found");
            return new TokenResponse
            {
                AccessToken = _tokenService.CreateAccessToken(user),
                RefreshToKen = _tokenService.CreateRefreshToken(user)
            };
        }

        public TokenResponse HandleRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
