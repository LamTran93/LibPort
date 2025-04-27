namespace LibPort.Models
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class BaseEntity<T> : BaseEntity
    {
        public T Id { get; set; }
    }
}
