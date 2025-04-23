namespace LibPort.Models
{
    public class Book : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
