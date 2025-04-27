namespace LibPort.Exceptions
{
    public class NotValidIdException : Exception
    {
        public NotValidIdException() : base("Id is not valid") { }
        public NotValidIdException(string message) : base(message) { }
    }
}
