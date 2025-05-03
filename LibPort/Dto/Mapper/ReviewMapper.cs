using LibPort.Dto.Response;
using LibPort.Models;

namespace LibPort.Dto.Mapper
{
    public static class ReviewMapper
    {
        public static Review ToEntity(this ShowReview show)
        {
            return new Review
            {
                Id = show.Id,
                Rating = show.Rating,
                Comment = show.Comment,
            };
        }

        public static ShowReview ToShow(this Review review)
        {
            return new ShowReview
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
            };
        }
    }
}
