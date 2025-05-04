namespace LibPort.Models
{
    public class BookBorrowingRequest : BaseEntity<Guid>
    {
        public Guid RequestorId { get; set; }
        public Guid? ApproverId { get; set; }
        public DateTime RequestedDate { get; set; }
        public Status Status { get; set; }

        public ICollection<BookBorrowingRequestDetails> Details { get; set; }
        public User Requestor { get; set; }
        public User Approver { get; set; }
    }   
}
