using Backend.Models.Base;
using Backend.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class User: BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public double Balanta_Cont { get; set; } = 100.50;

        public Role Role { get; set; } = Role.User;

        public Cos_Cumparaturi? Cos {  get; set; }
    }
}
