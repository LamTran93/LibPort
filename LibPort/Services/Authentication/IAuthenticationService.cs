using LibPort.Models;

namespace LibPort.Services.Authentication
{
    public interface IAuthenticationService
    {
        public Task<TokenPackage> HandleLogin(string username, string password);
        public Task<User> HandleRegister(User user);
        public TokenPackage HandleRefreshToken(string refreshToken);
    }
}
