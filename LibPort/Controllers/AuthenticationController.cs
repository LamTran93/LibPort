using LibPort.Dto.Mapper;
using LibPort.Dto.Request;
using LibPort.Exceptions;
using LibPort.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LibPort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenPackage>> Login(LoginInfo info)
        {
            var tokens = await _authenticationService.HandleLogin(info.Username, info.Password);

            return Ok(tokens);
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<TokenPackage>> Refresh(RequestToken tokens)
        {
            if (string.IsNullOrWhiteSpace(tokens.RefreshToken))
            {
                return BadRequest("No token found");
            }
            return Ok(_authenticationService.HandleRefreshToken(tokens.RefreshToken));
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RequestUser request)
        {
            var user = request.ToEntity();
            await _authenticationService.HandleRegister(user);
            return Created();
        }
    }
}
