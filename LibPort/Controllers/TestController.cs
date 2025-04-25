using LibPort.Models;
using LibPort.Services.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibPort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TestController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
