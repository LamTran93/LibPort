﻿using LibPort.Dto.Mapper;
using LibPort.Dto.Request;
using LibPort.Dto.Response;
using LibPort.Services.BookService;
using LibPort.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibPort.Services.BorrowingRequest;
using LibPort.Models;
using LibPort.Services.UserService;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using LibPort.Exceptions;

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
        private readonly IUserService _userService;

        public AdminController(
            IBookService bookService,
            ICategoryService categoryService,
            IBorrowingRequestService borrowingService,
            IUserService userService
            )
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _borrowingService = borrowingService;
            _userService = userService;
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
            if (string.IsNullOrWhiteSpace(category.Name))
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

        [HttpGet("users")]
        public async Task<ActionResult<List<ShowUser>>> GetUsers([FromQuery] string? keyword)
        {
            List<User> users;
            if (string.IsNullOrWhiteSpace(keyword))
                users = await _userService.ListAsync();
            else users = await _userService
                .ListWhereAsync(u => EF.Functions.Like(u.Username, $"%{keyword}%"));
            return Ok(users.Select(u => u.ToShow()).ToList());
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<ShowUser>> GetUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out var userId))
                return BadRequest("Not an valid Id");
            var user = await _userService.GetAsync(userId);
            if (user == null) throw new NotFoundException($"User id {id} not found");
            return Ok(user.ToShow());
        }

        [HttpPost("users")]
        public async Task<ActionResult<ShowUser>> CreateUser(RequestUser user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                return BadRequest("Username is required");
            if (string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Password is required");
            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("Email is required");
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(user.Email))
                return BadRequest("Invalid email format");

            var entity = await _userService.CreateAsync(user.ToEntity());
            return CreatedAtAction("GetCategory", new { id = entity.Id }, entity);

        }

        [HttpPut("users/{id}")]
        public async Task<ActionResult> EditUser(string id, ShowUser user)
        {
            if (!id.Equals(user.Id.ToString())) return BadRequest();

            await _userService.UpdateAsync(user.ToEntity());
            return NoContent();

        }

        [HttpDelete("users/{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (!Guid.TryParse(id, out var userId))
                return BadRequest("Not a valid Id");
            await _userService.DeleteAsync(userId);
            return NoContent();
        }

        [HttpPut("users/{id}/roles")]
        public async Task<ActionResult> AssignRole(string id, [FromQuery] UserType type)
        {
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out var userId))
                return BadRequest("Not a valid Id");
            await _userService.AssignRole(userId, type);
            return NoContent();
        }
    }
}
