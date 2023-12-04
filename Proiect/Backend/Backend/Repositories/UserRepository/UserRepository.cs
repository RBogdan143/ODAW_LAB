using Backend.Data;
using Backend.Helpers.Extensions;
using Backend.Models;
using Backend.Repositories.GenericRepository;

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
    }
}
