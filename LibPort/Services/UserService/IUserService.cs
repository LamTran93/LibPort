using LibPort.Models;
using System.Linq.Expressions;

namespace LibPort.Services.UserService
{
    public interface IUserService
    {
        public Task<List<User>> List(Expression<Func<User, bool>> predicate);
        public Task<List<User>> ListAsync();
        public Task<User?> GetAsync(Guid id);
        public Task<User> CreateAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(Guid id);
    }
}
