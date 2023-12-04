using Backend.Models;
using Backend.Repositories.GenericRepository;

namespace Backend.Repositories.UserRepository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        User FindByUsername(string username);

        List<User> FindAllActive();
    }
}
