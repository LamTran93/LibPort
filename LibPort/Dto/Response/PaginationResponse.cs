namespace LibPort.Dto.Response
{
    public class PaginationResponse<TEntity>
    {
        public int First { get; set; }
        public int Last { get; set; }
        public int Current { get; set; }
        public int Prev { get; set; }
        public int Next { get; set; }
        public int Total { get; set; }
        public int Pages { get; set; }
        public List<TEntity> Items { get; set; }
    }
}
