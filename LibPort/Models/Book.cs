namespace LibPort.Models
{
    public class Book : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
