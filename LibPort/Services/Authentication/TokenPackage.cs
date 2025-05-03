using LibPort.Dto.Response;

namespace LibPort.Services.Authentication
{
    public class TokenPackage
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public ShowUser User { get; set; }
    }
}
