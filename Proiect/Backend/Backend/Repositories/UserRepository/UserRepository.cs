using Backend.Data;
using Backend.Helpers.Extensions;
using Backend.Models;
using Backend.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BackendContext backendContext) : base(backendContext)
        {
        }

        public List<User> FindAllActive()
        {
            return _table.GetActiveUsers().ToList();
        }

        public User FindByUsername(string username)
        {
            return _table.FirstOrDefault(u => u.Username.Equals(username));
        }

        public async Task UpdateAsync(User user)
        {
            _backendContext.Update(user);
            await _backendContext.SaveChangesAsync();
        }

        public async Task AddAsync(User user)
        {
            await _backendContext.AddAsync(user);
            await _backendContext.SaveChangesAsync();
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _table.FirstOrDefaultAsync(u => u.Username.Equals(username) && u.Password.Equals(password));
            return user;
        }
    }
}
