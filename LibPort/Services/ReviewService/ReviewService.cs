using LibPort.Contexts;
using LibPort.Exceptions;
using LibPort.Models;
using Microsoft.EntityFrameworkCore;

namespace LibPort.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly LibraryContext _context;

        public ReviewService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            review.Id = Guid.Empty;
            var bookExisted = await _context.Books.AnyAsync(b => b.Id == review.BookId);
            if (!bookExisted) throw new NotFoundException($"Book id {review.BookId} not found");
            var alreadyReviewed = await _context.Reviews
                .AnyAsync(r => r.UserId == review.UserId && r.BookId == review.Id);
            if (alreadyReviewed) throw new DataConflictException("Already review this book");
            var contextReview = await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return contextReview.Entity;
        }

        public async Task<List<Review>> GetReviewsAsync(Guid bookId)
        {
            return await _context.Reviews.Include(r => r.User).Where(r => r.BookId == bookId).ToListAsync();
        }
    }
}
