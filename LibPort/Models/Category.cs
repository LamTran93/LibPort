namespace LibPort.Models
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
