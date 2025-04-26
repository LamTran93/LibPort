using LibPort.Contexts;
using LibPort.Exceptions;
using LibPort.Models;
using Microsoft.EntityFrameworkCore;

namespace LibPort.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryContext _context;

        public CategoryService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            category.Id = default;
            var newCategory = _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return newCategory.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var dbCategory = await _context.Categories.SingleOrDefaultAsync(b => b.Id.Equals(id));
            if (dbCategory == null) throw new NotFoundException($"Category id {id} not found");
            _context.Categories.Remove(dbCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> GetAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public List<Category> List(Func<Category, bool> predicate)
        {
            return _context.Categories.Where(predicate).ToList();
        }

        public async Task<List<Category>> ListAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            var isCategoryExisted = await _context.Categories.AnyAsync(b => b.Id.Equals(category.Id));
            if (!isCategoryExisted) throw new NotFoundException($"Category id {category.Id} not found");
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
