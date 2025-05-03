namespace LibPort.Dto.Request
{
    public class FilterOption
    {
        public string? Title { get; set; }
        public bool? IsAvailable { get; set; }
        public int? MinimumRating { get; set; }
        public int? CategoryId { get; set; }
    }
}
