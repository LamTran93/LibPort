using LibPort.Models;
using System.Linq.Expressions;

namespace LibPort.Services.BookService
{
    public interface IBookService
    {
        public Task<List<Book>> ListWhereAsync(Expression<Func<Book, bool>> predicate);
        public Task<List<Book>> ListAsync();
        public Task<Book?> GetAsync(Guid id);
        public Task<Book> CreateAsync(Book book);
        public Task UpdateAsync(Book book);
        public Task DeleteAsync(Guid id);
    }
}
