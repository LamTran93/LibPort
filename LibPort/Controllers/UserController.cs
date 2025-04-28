using LibPort.Models;
using LibPort.Services.BookService;
using LibPort.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibPort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "NormalUserOnly")]
    public class UserController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public UserController(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }
    }
}
