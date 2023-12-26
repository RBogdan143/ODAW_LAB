using AutoMapper;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.Enums;
using Backend.Repositories.UserRepository;

namespace Backend.Services.UserService
{
    public class DatabaseService: IUserService
    {
        public IUserRepository _userRepository;
        public IMapper _mapper;

        public DatabaseService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<List<UserDto>> GetAllUsers()
        {
            var userList = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(userList);
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id.ToString());
            return _mapper.Map<User>(user);
        }

        public UserDto GetUserByUsername(string username)
        {
            var user = _userRepository.FindByUsername(username);

            //var userDto = new UserDto
            //{
            //    Username = user.Username,
            //    Email = user.Email,
            //    FullName = user.FirstName + user.LastName
            //};

            return _mapper.Map<UserDto>(user);
        }

        public async Task AssignRoleToUser(string username, Role role)
        {
            var user = _userRepository.FindByUsername(username);
            if (user != null)
            {
                user.Role = role;
                user.DateModified = DateTime.Now;
                await _userRepository.UpdateAsync(user);
            }
            else
            {
                throw new Exception("Utilizatorul nu a fost găsit.");
            }
        }

        public async Task RemoveRoleFromUser(string username, Role role)
        {
            var user = _userRepository.FindByUsername(username);
            if (user != null)
            {
                user.Role = Role.User; // Setează rolul implicit după eliminarea rolului de admin
                user.DateModified = DateTime.Now;
                await _userRepository.UpdateAsync(user);
            }
            else
            {
                throw new Exception("Utilizatorul nu a fost găsit.");
            }
        }

        public async Task<bool> CreateUserAsync(User users)
        {
            var user = _mapper.Map<User>(users);
            try
            {
                await _userRepository.AddAsync(user);
                return true; // Returnează true dacă adăugarea a fost un succes
            }
            catch
            {
                return false; // Returnează false dacă a apărut o excepție
            }
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.AuthenticateAsync(username, password);
            return _mapper.Map<User>(user);
        }

        public async Task<bool> DeleteUser(string username)
        {
            var user = _userRepository.FindByUsername(username);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
                return true; // Returnează true dacă ștergerea a fost un succes
            }
            return false; // Returnează false dacă utilizatorul nu a fost găsit
        }

        public async Task<bool> UpdateUserProfile(string userId, UpdateProfileModel model)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                // Actualizează doar câmpurile care nu sunt nule în DTO
                user.Email = model.Email ?? user.Email;
                user.Password = model.Password ?? user.Password;
                user.Username = model.Username ?? user.Username;
                user.LastName = model.LastName ?? user.LastName;
                user.FirstName = model.FirstName ?? user.FirstName;
                user.DateModified = DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);
                return true; // Returnează true dacă actualizarea a fost un succes
            }
            return false; // Returnează false dacă utilizatorul nu a fost găsit
        }
    }
}