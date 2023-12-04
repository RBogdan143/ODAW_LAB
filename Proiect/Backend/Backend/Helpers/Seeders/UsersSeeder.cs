using Backend.Data;
using Backend.Models;
using Backend.Models.Enums;

namespace Backend.Helpers.Seeders
{
    public class UsersSeeder
    {
        public readonly BackendContext _backendContext;

        public UsersSeeder(BackendContext backendContext)
        {
            _backendContext = backendContext;
        }

        public void SeedInitialUsers()
        {
            if(!_backendContext.Users.Any())
            {
                var user1 = new User
                {
                    FirstName = "Fist name User 1",
                    LastName = "Last name User 1",
                    IsDeleted = false,
                    Email = "user1@mail.com",
                    Username = "user1",
                    Password = "0000",
                    Role = Role.Admin
                };

                var user2 = new User
                {
                    FirstName = "Fist name User 2",
                    LastName = "Last name User 2",
                    IsDeleted = false,
                    Email = "user2@mail.com",
                    Username = "user2",
                    Password = "0000"
                };

                _backendContext.Users.Add(user1);
                _backendContext.Users.Add(user2);

                _backendContext.SaveChanges();
            }
        }
    }
}
