using LibPort.Dto.Request;
using LibPort.Dto.Response;
using LibPort.Exceptions;
using LibPort.Models;

namespace LibPort.Dto.Mapper
{
    public static class BookMapper
    {
        public static ShowBook ToShow(this Book book)
        {
            return new ShowBook
            {
                Id = book.Id,
                Author = book.Author,
                Category = book.Category?.ToShow(),
                Title = book.Title,
                Quantity = book.Quantity,
                Total = book.Total,
                Description = book.Description,
                Reviews = book.Reviews?.Select(r => r.ToShow()).ToList(),
                RatingAverage = book.Reviews?.Count > 0 ? book.Reviews.Average(r => r.Rating) : 0
            };
        }

        public static Book ToEntity(this ShowBook show)
        {
            return new Book
            {
                Id = show.Id,
                Author = show.Author,
                Category = show.Category?.ToEntity(),
                Title = show.Title,
                Quantity = show.Quantity,
                Total = show.Total,
                Description = show.Description,
                Reviews = show.Reviews.Select(r => r.ToEntity()).ToList()
            };
        }

        public static Book ToEntity(this RequestBook request)
        {
            if (!Guid.TryParse(request.Id, out var id)) throw new NotValidIdException("Id is not guid");
            return new Book
            {
                Id = id,
                Author = request.Author,
                CategoryId = request.CategoryId,
                Title = request.Title,
                Quantity = request.Quantity,
                Total = request.Total,
                Description = request.Description,
            };
        }
    }
}
