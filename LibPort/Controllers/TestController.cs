using LibPort.Contexts;
using LibPort.Models;
using LibPort.Services.Authentication;
using LibPort.Services.BookService;
using Microsoft.AspNetCore.Mvc;

namespace LibPort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly LibraryContext _context;

        public TestController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user1 = new User { Username = "user", Password = "1234", UserType = UserType.NormalUser, Email = "user@libport.com" };
            var user2 = new User { Username = "admin", Password = "1234", UserType = UserType.SuperUser, Email = "admin@libport.com" };
            _context.Users.AddRange([user1, user2]);
            _context.SaveChanges();
            return Ok();
        }
    }
}
