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
        public async Task<ActionResult> Login(LoginInfo info)
        {
            var tokens = await _authenticationService.HandleLogin(info.Username, info.Password);

            HttpContext.Response.Cookies.Append("RefreshToken", tokens.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(12)
            });
            return Ok(new { tokens.AccessToken });
        }

        [HttpGet("refreshToken")]
        public async Task<ActionResult> Refresh()
        {
            var token = HttpContext.Request.Cookies
                .FirstOrDefault(c => c.Key == "RefreshToken");
            if (token.Key == null || token.Value == null)
            {
                return BadRequest("No token found");
            }
            return Ok(new { _authenticationService.HandleRefreshToken(token.Value).AccessToken });
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
