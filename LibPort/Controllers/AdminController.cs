using LibPort.Dto.Mapper;
using LibPort.Dto.Request;
using LibPort.Dto.Response;
using LibPort.Services.BookService;
using LibPort.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LibPort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public AdminController(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        [HttpGet("books")]
        public async Task<ActionResult<List<ShowBook>>> ListAllBooks()
        {
            try
            {
                var allBooks = await _bookService.ListAsync();
                return allBooks.Select(b => b.ToShow()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting books");
            }

        }

        [HttpGet("books/{id}")]
        public async Task<ActionResult<ShowBook>> GetBook(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var bookId))
                    return BadRequest("Not a valid Id");

                var book = await _bookService.GetAsync(bookId);

                if (book == null) return NotFound();

                return Ok(book.ToShow());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting book");
            }
        }

        [HttpPost("books")]
        public async Task<ActionResult<ShowBook>> CreateBook(RequestBook request)
        {
            request.Id = Guid.Empty;
            var book = request.ToEntity();

            try
            {
                var entity = await _bookService.CreateAsync(book);
                return CreatedAtAction("GetBook", new { id = entity.Id }, entity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating book");
            }
        }

        [HttpPut("books/{id}")]
        public async Task<ActionResult> EditBook(string id, RequestBook request)
        {
            if (request.Id.ToString() != id) return BadRequest();
        }
    }
}
