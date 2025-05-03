using LibPort.Models;
using System.Linq.Expressions;

namespace LibPort.Services.BorrowingRequest
{
    public interface IBorrowingRequestService
    {
        public Task<List<BookBorrowingRequest>> ListAsync();
        public Task<List<BookBorrowingRequest>> ListWhereAsync(Expression<Func<BookBorrowingRequest, bool>> predicate);
        public Task<BookBorrowingRequest> GetAsync(Guid id);
        public Task<BookBorrowingRequest> CreateAsync(BookBorrowingRequest request);
        public Task ApproveRequest(Guid id);
        public Task RejectRequest(Guid id);
        public Task RequestBorrowingBooks(Guid userId, List<Guid> bookIds);
    }
}
