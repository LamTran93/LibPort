using LibPort.Dto.Request;
using LibPort.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LibPort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("login")]
        public async Task<ActionResult<TokenResponse>> Login(LoginInfo info)
        {
            var response = _authenticationService.HandleLogin(info.Username, info.Password);
            return Ok(response);
        }
    }
}
