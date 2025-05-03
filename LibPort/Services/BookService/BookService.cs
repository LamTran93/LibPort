using LibPort.Contexts;
using LibPort.Dto.Request;
using LibPort.Dto.Response;
using LibPort.Exceptions;
using LibPort.Models;
using Microsoft.EntityFrameworkCore;

namespace LibPort.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<PaginationResponse<Book>> GetPaginationAsync(int page, int perPage)
        {
            var result = new PaginationResponse<Book>();
            result.Items = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Reviews)
                .OrderBy(x => x.Title)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync();
            var bookQuantity = await _context.Books.CountAsync();
            result.Pages = bookQuantity / perPage;
            result.Last = result.Pages;
            result.Total = bookQuantity;
            result.First = 1;
            result.Current = page;
            result.Next = page == result.Last ? (result.Last) : (page + 1);
            result.Prev = page == result.First ? (result.First) : (page - 1);
            return result;
        }

        public async Task<PaginationResponse<Book>> FilterAsync(FilterOption options, int page, int perPage)
        {
            var result = new PaginationResponse<Book>();
            var items = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Reviews)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(options.Title))
                items = items.Where(b => EF.Functions.Like(b.Title, $"%{options.Title}%"));
            if (options.CategoryId != null)
                items = items.Where(b => b.CategoryId == options.CategoryId);
            if (options.IsAvailable == true)
                items = items.Where(b => b.Quantity > 0);
            else if (options.IsAvailable == false)
                items = items.Where(b => b.Quantity == 0);
            if (options.MinimumRating != null)
                items = items.Where(b => b.Reviews.Average(r => r.Rating) <= 0 || b.Reviews.Average(r => r.Rating) > options.MinimumRating);
            result.Items = await items
                .OrderBy(x => x.Title)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync();
            var bookQuantity = await items.CountAsync();
            result.Pages = bookQuantity / perPage;
            result.Last = result.Pages;
            result.Total = bookQuantity;
            result.First = 1;
            result.Current = page;
            result.Next = page == result.Last ? (result.Last) : (page + 1);
            result.Prev = page == result.First ? (result.First) : (page - 1);
            return result;
        }

        public async Task<List<Book>> ListAsync()
        {
            return await _context.Books.Include(b => b.Reviews).ToListAsync();
        }

        public async Task<Book?> GetAsync(Guid id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> CreateAsync(Book book)
        {
            book.Id = Guid.Empty;
            var newBook = _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return newBook.Entity;
        }

        public async Task UpdateAsync(Book book)
        {
            var isBookExisted = await _context.Books.AnyAsync(b => b.Id.Equals(book.Id));
            if (!isBookExisted) throw new NotFoundException($"Book id {book.Id} not found");
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var dbBook = await _context.Books.SingleOrDefaultAsync(b => b.Id.Equals(id));
            if (dbBook == null) throw new NotFoundException($"Book id {id} not found");
            _context.Books.Remove(dbBook);
            await _context.SaveChangesAsync();
        }
    }
}
