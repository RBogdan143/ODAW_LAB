using Backend.Models.Base;

namespace Backend.Models
{
    public class RegisterModel: BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
