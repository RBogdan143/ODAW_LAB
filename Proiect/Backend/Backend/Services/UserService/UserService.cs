using AutoMapper;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.Enums;
using Backend.Repositories.UserRepository;

namespace Backend.Services.UserService
{
    public class UserService: IUserService
    {
        public IUserRepository _userRepository;
        public IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<List<UserDto>> GetAllUsers()
        {
            var userList = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(userList);
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
                user.Role = role; // Presupunând că aveți o proprietate Role în modelul User
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
    }
}