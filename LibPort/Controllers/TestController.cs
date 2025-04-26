using LibPort.Contexts;
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
        private readonly LibraryContext _context;

        public TestController(ITokenService tokenService, LibraryContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user1 = new User
            { Username = "lamtran", Password = "12345678", Email = "lamtran@yahoo.com", UserType = UserType.NormalUser };
            var user2 = new User
            { Username = "admin", Password = "12345678", Email = "lamtran@yahoo.com", UserType = UserType.SuperUser };
            _context.Users.AddRange([user1, user2]);
            _context.SaveChanges();
            return Ok();
        }
    }
}
