namespace LibPort.Exceptions
{
    public class DataConflictException: Exception
    {
        public DataConflictException() : base() { }
        public DataConflictException(string message) : base(message) { }
    }
}
