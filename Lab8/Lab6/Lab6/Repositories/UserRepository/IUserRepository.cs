using Lab6.Models;
using Lab6.Repositories.GenericRepository;

namespace Lab6.Repositories.UserRepository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        User FindByUsername(string username);

        List<User> FindAllActive();
    }
}
