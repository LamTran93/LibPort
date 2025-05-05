using LibPort.Contexts;
using LibPort.Exceptions;
using LibPort.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibPort.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _context;

        public UserService(LibraryContext libraryContext)
        {
            _context = libraryContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            user.Id = Guid.Empty;
            var newUser = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return newUser.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var dbUser = await _context.Users.SingleOrDefaultAsync(b => b.Id.Equals(id));
            if (dbUser == null) throw new NotFoundException($"User id {id} not found");
            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> List(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.Where(predicate).ToListAsync();
        }

        public async Task<List<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var dbUser = await _context.Users.FindAsync(user.Id);
            if (dbUser == null) throw new NotFoundException($"User id {user.Id} not found");
            dbUser.Username = user.Username;
            dbUser.Email = user.Email;
            dbUser.UserType = user.UserType;
            await _context.SaveChangesAsync();
        }
    }
}
