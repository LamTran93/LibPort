namespace LibPort.Exceptions
{
    public class InvalidBorrowingRequestException : Exception
    {
        public InvalidBorrowingRequestException() : base() { }
        public InvalidBorrowingRequestException(string message) : base(message) { }
    }
}
