using LibPort.Dto.Request;
using LibPort.Dto.Response;
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
                Category = book.Category.ToShow(),
                Title = book.Title,
                Description = book.Description,
            };
        }

        public static Book ToEntity(this ShowBook show)
        {
            return new Book
            {
                Id = show.Id,
                Author = show.Author,
                Category = show.Category.ToEntity(),
                Title = show.Title,
                Description = show.Description,
            };
        }

        public static Book ToEntity(this RequestBook request)
        {
            Guid.TryParse(request.Id, out var id);
            return new Book
            {
                Id = id,
                Author = request.Author,
                CategoryId = request.CategoryId,
                Title = request.Title,
                Description = request.Description,
            };
        }
    }
}
