using LibPort.Contexts;
using LibPort.Models;

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
            var contextReview = await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return contextReview.Entity;
        }
    }
}
