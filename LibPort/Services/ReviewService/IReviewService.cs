using LibPort.Models;

namespace LibPort.Services.ReviewService
{
    public interface IReviewService
    {
        public Task<Review> AddReviewAsync(Review review);
    }
}
