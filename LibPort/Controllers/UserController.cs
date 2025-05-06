using LibPort.Dto.Mapper;
using LibPort.Dto.Request;
using LibPort.Dto.Response;
using LibPort.Exceptions;
using LibPort.Models;
using LibPort.Services.BookService;
using LibPort.Services.BorrowingRequest;
using LibPort.Services.CategoryService;
using LibPort.Services.ReviewService;
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
        private readonly IBorrowingRequestService _borrowingRequestService;
        private readonly IReviewService _reviewService;

        public UserController(
            IBookService bookService,
            ICategoryService categoryService,
            IBorrowingRequestService borrowingRequestService,
            IReviewService reviewService
            )
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _borrowingRequestService = borrowingRequestService;
            _reviewService = reviewService;
        }

        [HttpGet("books")]
        public async Task<ActionResult> ListBooks(
            [FromQuery] string? _page,
            [FromQuery] string? _perPage
            )
        {
            if (string.IsNullOrWhiteSpace(_page))
            {
                var allBooks = await _bookService.ListAsync();
                return Ok(allBooks.Select(b => b.ToShow()).ToList());
            }
            else
            {
                var pageParseSuccess = int.TryParse(_page, out var page);
                var perPageParseSuccess = int.TryParse(_perPage, out var perPage);
                if (!(pageParseSuccess && perPageParseSuccess)) return BadRequest("Could not parse the query");
                var pagination = await _bookService.GetPaginationAsync(page, perPage);
                var result = new PaginationResponse<ShowBook>
                {
                    First = pagination.First,
                    Last = pagination.Last,
                    Current = pagination.Current,
                    Next = pagination.Next,
                    Prev = pagination.Prev,
                    Total = pagination.Total,
                    Pages = pagination.Pages,
                    Items = pagination.Items.Select(b => b.ToShow()).ToList()
                };
                return Ok(result);
            }
        }

        [HttpGet("books/{id}")]
        public async Task<ActionResult<ShowBook>> GetBook(string id)
        {
            if (!Guid.TryParse(id, out var bookId)) return BadRequest("Not a valid Id");
            var book = await _bookService.GetAsync(bookId);
            if (book == null) throw new NotFoundException($"Book id {id} not found");
            return Ok(book.ToShow());
        }

        [HttpGet("books/filter")]
        public async Task<ActionResult> FilterBooks(
            [FromQuery] string? _page,
            [FromQuery] string? _perPage,
            [FromQuery] string? title,
            [FromQuery] string? author,
            [FromQuery] string? isAvailable,
            [FromQuery] string? minimumRating,
            [FromQuery] string? categoryId
            )
        {
            if (string.IsNullOrWhiteSpace(_page)) { _page = "1"; }
            if (string.IsNullOrWhiteSpace(_perPage)) { _perPage = "10"; }
            var options = new FilterOption();
            options.Title = title;
            options.Author = author;
            if (bool.TryParse(isAvailable, out var parsedAvailable))
                options.IsAvailable = parsedAvailable;
            if (int.TryParse(minimumRating, out var parsedMinimumRating))
                options.MinimumRating = parsedMinimumRating;
            if (int.TryParse(categoryId, out var parsedCategoryId))
                options.CategoryId = parsedCategoryId;

            var pageParseSuccess = int.TryParse(_page, out var page);
            var perPageParseSuccess = int.TryParse(_perPage, out var perPage);
            if (!(pageParseSuccess && perPageParseSuccess)) return BadRequest("Could not parse the query");
            var pagination = await _bookService.FilterAsync(options, page, perPage);
            var result = new PaginationResponse<ShowBook>
            {
                First = pagination.First,
                Last = pagination.Last,
                Current = pagination.Current,
                Next = pagination.Next,
                Prev = pagination.Prev,
                Total = pagination.Total,
                Pages = pagination.Pages,
                Items = pagination.Items.Select(b => b.ToShow()).ToList()
            };
            return Ok(result);

        }

        [HttpPost("requests")]
        public async Task<ActionResult<ShowBookBorrowingRequest>> RequestBook(List<string> bookIds)
        {
            var userId = HttpContext.User.FindFirst("user_id")?.Value;
            if (userId == null) return Unauthorized("User Id not found");
            var userGuid = Guid.Parse(userId);
            var bookIdList = new List<Guid>();
            foreach (var bookId in bookIds)
            {
                var parseBookIdSuccess = Guid.TryParse(bookId, out var result);
                if (!parseBookIdSuccess)
                    throw new NotValidIdException($"Book id {bookId} is not a Guid");
                bookIdList.Add(result);
            }

            await _borrowingRequestService.RequestBorrowingBooks(userGuid, bookIdList);
            return Created();
        }

        [HttpGet("requests")]
        public async Task<ActionResult<List<ShowBookBorrowingRequest>>> GetRequests()
        {
            var userId = HttpContext.User.FindFirst("user_id")?.Value;
            if (userId == null) return Unauthorized("User Id not found");
            var userGuid = Guid.Parse(userId);
            var requestList = await _borrowingRequestService.ListWhereAsync(r => r.RequestorId == userGuid);
            return requestList.Select(r => r.ToShow()).ToList();
        }

        [HttpGet("categories")]
        public async Task<ActionResult<List<ShowCategory>>> ListAllCategories()
        {
            var allCategories = await _categoryService.ListAsync();
            return allCategories.Select(b => b.ToShow()).ToList();
        }

        [HttpPost("reviews")]
        public async Task<ActionResult<ShowReview>> CreateReview(RequestReview request)
        {
            request.UserId = Guid.Parse(HttpContext.User.FindFirst("user_id")!.Value);
            await _reviewService.AddReviewAsync(request.ToEntity());
            return Created();
        }

        [HttpGet("reviews/{bookId}")]
        public async Task<ActionResult<List<ShowReview>>> GetReviews(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId) || !Guid.TryParse(bookId, out var id))
                return BadRequest("Invalid book id");
            var reviewList = await _reviewService.GetReviewsAsync(id);
            return reviewList.Select(r => r.ToShow()).ToList();
        }
    }
}
