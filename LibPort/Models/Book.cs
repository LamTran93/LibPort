namespace LibPort.Models
{
    public class Book : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public byte[] Version { get; set; }
    }
}
