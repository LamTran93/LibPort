namespace LibPort.Models
{
    public class User : BaseEntity<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }

        public ICollection<BookBorrowingRequest> BorrowingRequests { get; set; }
    }
}
