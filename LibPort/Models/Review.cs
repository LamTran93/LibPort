namespace LibPort.Models
{
    public class Review : BaseEntity<Guid>
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public Guid BookId { get; set; }

        public Book Book { get; set; }
    }
}
