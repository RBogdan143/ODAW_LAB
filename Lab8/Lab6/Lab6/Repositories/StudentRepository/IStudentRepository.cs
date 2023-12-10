using Lab6.Models;
using Lab6.Repositories.GenericRepository;

namespace Lab6.Repositories.StudentRepository
{
    public interface IStudentRepository: IGenericRepository<Student>
    {
        List<Student> OrderByAge(int age);
    }
}
