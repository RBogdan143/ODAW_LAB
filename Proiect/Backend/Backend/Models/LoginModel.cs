using Backend.Models.Base;

namespace Backend.Models
{
    public class LoginModel: BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
