namespace LibPort.Exceptions
{
    public class TokenInvalidException : Exception
    {
        public TokenInvalidException() : base("Invalid token") { }
        public TokenInvalidException(string message) : base(message) { }
    }
}
