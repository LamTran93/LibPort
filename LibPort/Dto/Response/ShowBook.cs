namespace LibPort.Dto.Response
{
    public class ShowBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public double RatingAverage { get; set; }
        public List<ShowReview> Reviews { get; set; }

        public ShowCategory Category { get; set; }
    }
}
