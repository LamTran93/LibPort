using LibPort.Dto.Response;
using LibPort.Models;

namespace LibPort.Dto.Mapper
{
    public static class CategoryMapper
    {
        public static ShowCategory ToShow(this Category category)
        {
            return new ShowCategory { Id = category.Id, Name = category.Name };
        }

        public static Category ToEntity(this ShowCategory show)
        {
            return new Category { Id = show.Id, Name = show.Name };
        }
    }
}
