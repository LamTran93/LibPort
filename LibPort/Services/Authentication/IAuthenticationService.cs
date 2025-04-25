namespace LibPort.Services.Authentication
{
    public interface IAuthenticationService
    {
        public Task<TokenResponse> HandleLogin(string username, string password);
        public TokenResponse HandleRefreshToken(string refreshToken);
    }
}
