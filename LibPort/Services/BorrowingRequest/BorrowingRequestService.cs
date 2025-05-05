using Azure.Core;
using LibPort.Contexts;
using LibPort.Dto.Mapper;
using LibPort.Exceptions;
using LibPort.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Net;

namespace LibPort.Services.BorrowingRequest
{
    public class BorrowingRequestService : IBorrowingRequestService
    {
        private readonly LibraryContext _context;

        public BorrowingRequestService(LibraryContext context)
        {
            _context = context;
        }

        public async Task RequestBorrowingBooks(Guid userId, List<Guid> bookIds)
        {
            if (bookIds.Count > 5) throw new BookExceedLimitException($"Books quantity exceed limit ({bookIds.Count}/5");
            var thisMonthRequestCount = await _context.BookBorrowingRequests
                                                .Where(r => r.RequestedDate.Month == DateTime.Now.Month)
                                                .CountAsync();
            if (thisMonthRequestCount >= 3) throw new ExceedRequestLimitException("Book requests exceed limit(3/3)");

            var borrowingDetails = new List<BookBorrowingRequestDetails>();
            foreach (var bookId in bookIds)
            {
                borrowingDetails.Add(new BookBorrowingRequestDetails { BookId = bookId });
            }
            var borrowingRequest = new BookBorrowingRequest
            {
                RequestorId = userId,
                RequestedDate = DateTime.Now,
                Status = Status.Waiting,
                Details = borrowingDetails
            };
            await _context.AddAsync(borrowingRequest);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveRequest(Guid id)
        {
            try
            {
                var dbBorrowingRequest =
                await _context.BookBorrowingRequests
                    .Include(r => r.Details)
                    .ThenInclude(d => d.Book)
                    .FirstAsync(r => r.Id == id);
                if (dbBorrowingRequest == null) throw new NotFoundException($"Request id {id} not found");
                foreach (var book in dbBorrowingRequest.Details.Select(d => d.Book))
                {
                    if (book.Quantity == 0)
                        throw new NotEnoughBookException($"Book id {book.Id} is not avaiable");
                    book.Quantity -= 1;
                }

                dbBorrowingRequest.Status = Status.Approved;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    var databaseValues = await entry.GetDatabaseValuesAsync();
                    entry.OriginalValues.SetValues(databaseValues);
                }
            }
        }

        public async Task RejectRequest(Guid id)
        {
            var dbBorrowingRequest = await _context.BookBorrowingRequests.FindAsync(id);
            if (dbBorrowingRequest == null) throw new NotFoundException($"Request id {id} not found");
            dbBorrowingRequest.Status = Status.Rejected;
            await _context.SaveChangesAsync();
        }

        public async Task<BookBorrowingRequest> CreateAsync(BookBorrowingRequest borrowingRequest)
        {
            borrowingRequest.Id = Guid.Empty;
            var dbBorrowingRequest = await _context.AddAsync(borrowingRequest);
            await _context.SaveChangesAsync();
            return dbBorrowingRequest.Entity;
        }

        public async Task<BookBorrowingRequest> GetAsync(Guid id)
        {
            var request = await _context.BookBorrowingRequests.FindAsync(id);
            if (request == null) throw new NotFoundException($"Request id {id} not found");
            return request;
        }

        public async Task<List<BookBorrowingRequest>> ListAsync()
        {
            return await _context.BookBorrowingRequests
                .OrderBy(r => r.RequestedDate)
                .Include(r => r.Details)
                .ThenInclude(d => d.Book)
                .ThenInclude(b => b.Category)
                .ToListAsync();
        }

        public async Task<List<BookBorrowingRequest>> ListWhereAsync(Expression<Func<BookBorrowingRequest, bool>> predicate)
        {
            return await _context.BookBorrowingRequests
                .Include(r => r.Details)
                .ThenInclude(d => d.Book)
                .ThenInclude(b => b.Category)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
