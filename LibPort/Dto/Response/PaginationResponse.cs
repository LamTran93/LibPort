namespace LibPort.Dto.Response
{
    public class PaginationResponse<TEntity, TOrderBy>
    {
        public TOrderBy First { get; set; }
        public TOrderBy Last { get; set; }
        public TOrderBy Prev { get; set; }
        public TOrderBy Next { get; set; }
        public int Pages { get; set; }
        public int Items { get; set; }
        public List<TOrderBy> OrderBy { get; set; }
    }
}
