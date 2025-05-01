namespace LibPort.Exceptions
{
    public class NotEnoughBookException : Exception
    {
        public NotEnoughBookException() : base() { }
        public NotEnoughBookException(string message) : base(message) { }
    }
}
