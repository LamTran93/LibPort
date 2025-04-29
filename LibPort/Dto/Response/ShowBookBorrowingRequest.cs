using LibPort.Models;

namespace LibPort.Dto.Response
{
    public class ShowBookBorrowingRequest
    {
        public Guid Id { get; set; }
        public Guid RequestorId { get; set; }
        public Guid ApproverId { get; set; }
        public DateTime RequestedDate { get; set; }
        public Status Status { get; set; }

        public ICollection<ShowBook> Books { get; set; }
        public ShowUser Requestor { get; set; }
        public ShowUser Approver { get; set; }
    }
}
