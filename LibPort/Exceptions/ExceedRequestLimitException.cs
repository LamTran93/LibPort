namespace LibPort.Exceptions
{
    public class ExceedRequestLimitException : Exception
    {
        public ExceedRequestLimitException() : base() { }
        public ExceedRequestLimitException(string message) : base(message) { }
    }
}
