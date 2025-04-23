namespace LibPort.Models
{
    public class BookBorrowingRequestDetails : BaseEntity<int>
    {
        public Guid BookId { get; set; }
        public Guid RequestId { get; set; }

        public Book Book{ get; set; }
        public BookBorrowingRequest BookBorrowingRequest { get; set; }
    }
}
