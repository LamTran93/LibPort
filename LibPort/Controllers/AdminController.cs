using LibPort.Dto.Mapper;
using LibPort.Dto.Request;
using LibPort.Dto.Response;
using LibPort.Services.BookService;
using LibPort.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;
using LibPort.Models;

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
        public async Task<ActionResult<List<ShowBook>>> ListBooks(
            [FromQuery] string _page,
            [FromQuery] string _perPage
            )
        {
            var allBooks = await _bookService.ListAsync();
            return allBooks.Select(b => b.ToShow()).ToList();
        }

        [HttpGet("books/{id}")]
        public async Task<ActionResult<ShowBook>> GetBook(string id)
        {
            if (!Guid.TryParse(id, out var bookId))
                return BadRequest("Not a valid Id");

            var book = await _bookService.GetAsync(bookId);

            if (book == null) return NotFound();

            return Ok(book.ToShow());
        }

        [HttpPost("books")]
        public async Task<ActionResult<ShowBook>> CreateBook(RequestBook request)
        {
            request.Id = default;
            var book = request.ToEntity();

            var entity = await _bookService.CreateAsync(book);
            return CreatedAtAction("GetBook", new { id = entity.Id }, entity);

        }

        [HttpPut("books/{id}")]
        public async Task<ActionResult> EditBook(string id, RequestBook request)
        {
            if (!id.Equals(request.Id)) return BadRequest();

            await _bookService.UpdateAsync(request.ToEntity());
            return NoContent();

        }

        [HttpDelete("books/{id}")]
        public async Task<ActionResult> DeleteBook(string id)
        {
            if (!Guid.TryParse(id, out var bookId))
                return BadRequest("Not a valid Id");
            await _bookService.DeleteAsync(bookId);
            return NoContent();
        }

        [HttpGet("categories")]
        public async Task<ActionResult<List<ShowCategory>>> ListAllCategories()
        {
            var allCategories = await _categoryService.ListAsync();
            return allCategories.Select(b => b.ToShow()).ToList();
        }

        [HttpGet("categories/{id}")]
        public async Task<ActionResult<ShowCategory>> GetCategory(int id)
        {
            var category = await _categoryService.GetAsync(id);

            if (category == null) return NotFound();

            return Ok(category.ToShow());
        }

        [HttpPost("categories")]
        public async Task<ActionResult<ShowCategory>> CreateCategory(ShowCategory category)
        {
            if (string.IsNullOrEmpty(category.Name))
                return BadRequest("Category name is required");

            var entity = await _categoryService.CreateAsync(category.ToEntity());
            return CreatedAtAction("GetCategory", new { id = entity.Id }, entity);

        }

        [HttpPut("categories/{id}")]
        public async Task<ActionResult> EditCategory(string id, ShowCategory category)
        {
            if (!id.Equals(category.Id.ToString())) return BadRequest();

            await _categoryService.UpdateAsync(category.ToEntity());
            return NoContent();

        }

        [HttpDelete("categories/{id}")]
        public async Task<ActionResult> DeleteCategory(string id)
        {
            if (!int.TryParse(id, out var bookId))
                return BadRequest("Not a valid Id");
            await _categoryService.DeleteAsync(bookId);
            return NoContent();
        }

        [HttpGet("borrowing-requests")]
        public async Task<ActionResult<BookBorrowingRequest>> GetRequests()
        {

        }
    }
}
