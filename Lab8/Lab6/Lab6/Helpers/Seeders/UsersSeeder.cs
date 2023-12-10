using Lab6.Data;
using Lab6.Models;

namespace Lab6.Helpers.Seeders
{
    public class UsersSeeder
    {
        public readonly Lab6Context _lab6Context;

        public UsersSeeder(Lab6Context lab6Context)
        {
            _lab6Context = lab6Context;
        }

        public void SeedInitialUsers()
        {
            if(!_lab6Context.Users.Any())
            {
                var user1 = new User
                {
                    FirstName = "Fist name User 1",
                    LastName = "Last name User 1",
                    IsDeleted = false,
                    Email = "user1@mail.com",
                    Username = "user1",
                    Password = "0000"
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

                _lab6Context.Users.Add(user1);
                _lab6Context.Users.Add(user2);

                _lab6Context.SaveChanges();
            }
        }
    }
}
