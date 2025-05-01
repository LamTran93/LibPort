using LibPort.Dto.Mapper;
using LibPort.Dto.Request;
using LibPort.Dto.Response;
using LibPort.Services.BookService;
using LibPort.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibPort.Services.BorrowingRequest;

namespace LibPort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "SuperUserOnly")]
    public class AdminController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IBorrowingRequestService _borrowingService;

        public AdminController(IBookService bookService, ICategoryService categoryService, IBorrowingRequestService borrowingService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _borrowingService = borrowingService;
        }

        [HttpGet("books")]
        public async Task<ActionResult<List<ShowBook>>> ListBooks(
            [FromQuery] string? _page,
            [FromQuery] string? _perPage
            )
        {
            if (string.IsNullOrWhiteSpace(_page))
            {
                var allBooks = await _bookService.ListAsync();
                return allBooks.Select(b => b.ToShow()).ToList();
            }
            else
            {
                var pageParseSuccess = int.TryParse(_page, out var page);
                var perPageParseSuccess = int.TryParse(_perPage, out var perPage);
                if (!(pageParseSuccess && perPageParseSuccess)) return BadRequest("Could not parse the query");
                var pageBook = await _bookService.GetPagination(page, perPage);
                return pageBook.Select(b => b.ToShow()).ToList();
            }
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
        public async Task<ActionResult<List<ShowBookBorrowingRequest>>> GetRequests()
        {
            var requests = await _borrowingService.ListAsync();
            return requests.Select(r => r.ToShow()).ToList();
        }

        [HttpPut("borrowing-requests/{id}/approve")]
        public async Task<ActionResult> ApproveRequest(string id)
        {
            var parseSuccess = Guid.TryParse(id, out var requestId);
            if (!parseSuccess) return BadRequest("Not a vadid id");
            await _borrowingService.ApproveRequest(requestId);
            return NoContent();
        }

        [HttpPut("borrowing-requests/{id}/reject")]
        public async Task<ActionResult> RejectRequest(string id)
        {
            var parseSuccess = Guid.TryParse(id, out var requestId);
            if (!parseSuccess) return BadRequest("Not a vadid id");
            await _borrowingService.RejectRequest(requestId);
            return NoContent();
        }
    }
}
