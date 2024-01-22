using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.Enums;

namespace Backend.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();

        UserDto GetUserByUsername(string username);

        Task<User> GetById(Guid id);

        Task AssignRoleToUser(string username, Role role);
        Task RemoveRoleFromUser(string username);

        Task<bool> CreateUserAsync(User users);
        Task<User> AuthenticateAsync(string username, string password);
        Task<bool> DeleteUser(string username);
        Task<bool> UpdateUserProfile(string userId, UpdateProfileModel model);
    }
}
