using Backend.Models;
using Backend.Models.DTOs;

namespace Backend.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();

        UserDto GetUserByUsername(string username);
    }
}
