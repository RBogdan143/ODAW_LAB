using Lab6.Data;
using Lab6.Helpers.Extensions;
using Lab6.Models;
using Lab6.Repositories.GenericRepository;

namespace Lab6.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(Lab6Context lab6Context) : base(lab6Context)
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
