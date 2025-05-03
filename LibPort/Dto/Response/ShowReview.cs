using LibPort.Models;

namespace LibPort.Dto.Response
{
    public class ShowReview
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
