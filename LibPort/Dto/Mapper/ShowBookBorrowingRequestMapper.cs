using LibPort.Dto.Response;
using LibPort.Models;

namespace LibPort.Dto.Mapper
{
    public static class ShowBookBorrowingRequestMapper
    {
        public static BookBorrowingRequest ToEntity(this ShowBookBorrowingRequest show)
        {
            return new BookBorrowingRequest
            {
                Id = show.Id,
                RequestorId = show.RequestorId,
                ApproverId = show.ApproverId,
                RequestedDate = show.RequestedDate,
                Status = show.Status,
            };
        }

        public static ShowBookBorrowingRequest ToShow(this BookBorrowingRequest entity)
        {
            return new ShowBookBorrowingRequest
            {
                Id = entity.Id,
                RequestedDate = entity.RequestedDate,
                Status = entity.Status,
                Requestor = entity.Requestor?.ToShow(),
                Approver = entity.Approver?.ToShow(),
                Books = entity.Details.Select(d => d.Book?.ToShow()).ToList()
            };
        }
    }
}
