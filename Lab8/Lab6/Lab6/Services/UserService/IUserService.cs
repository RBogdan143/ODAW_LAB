using Lab6.Models;
using Lab6.Models.DTOs;

namespace Lab6.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();

        UserDto GetUserByUsername(string username);
    }
}
