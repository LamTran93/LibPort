using LibPort.Dto.Request;
using LibPort.Dto.Response;
using LibPort.Models;

namespace LibPort.Services.BookService
{
    public interface IBookService
    {
        public Task<PaginationResponse<Book>> GetPaginationAsync(int page, int perPage);
        public Task<PaginationResponse<Book>> FilterAsync(FilterOption options, int page, int perPage);
        public Task<List<Book>> ListAsync();
        public Task<Book?> GetAsync(Guid id);
        public Task<Book> CreateAsync(Book book);
        public Task UpdateAsync(Book book);
        public Task DeleteAsync(Guid id);
    }
}
