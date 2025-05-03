namespace LibPort.Exceptions
{
    public class BookExceedLimitException : Exception
    {
        public BookExceedLimitException() : base() { }
        public BookExceedLimitException(string message) : base(message) { }
    }
}
