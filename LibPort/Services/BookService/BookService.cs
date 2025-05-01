using LibPort.Contexts;
using LibPort.Dto.Response;
using LibPort.Exceptions;
using LibPort.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibPort.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetPagination(int page, int perPage)
        {
            return await _context.Books
                .OrderBy(x => x.Title)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync();
        }

        public async Task<PaginationResponse<Book, string>> ListPaginationAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> ListAsync()
        {
            return await _context.Books.ToListAsync();
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
