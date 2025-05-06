namespace LibPort.Dto.Request
{
    public class RequestReview
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public Guid BookId { get; set; }
    }
}
