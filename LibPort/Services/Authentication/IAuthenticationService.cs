namespace LibPort.Services.Authentication
{
    public interface IAuthenticationService
    {
        public Task<TokenPackage> HandleLogin(string username, string password);
        public TokenPackage HandleRefreshToken(string refreshToken);
    }
}
