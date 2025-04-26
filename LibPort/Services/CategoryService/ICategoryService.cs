using LibPort.Models;

namespace LibPort.Services.CategoryService
{
    public interface ICategoryService
    {
        public List<Category> List(Func<Category, bool> predicate);
        public Task<List<Category>> ListAsync();
        public Task<Category?> GetAsync(int id);
        public Task<Category> CreateAsync(Category book);
        public Task UpdateAsync(Category book);
        public Task DeleteAsync(int id);
    }
}
