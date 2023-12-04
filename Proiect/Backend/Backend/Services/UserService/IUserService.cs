using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.Enums;

namespace Backend.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();

        UserDto GetUserByUsername(string username);

        Task AssignRoleToUser(string username, Role role);
        Task RemoveRoleFromUser(string username, Role role);

        Task<bool> CreateUserAsync(User users);
        Task<User> AuthenticateAsync(string username, string password);
    }
}
