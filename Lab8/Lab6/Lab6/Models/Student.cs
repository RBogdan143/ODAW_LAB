using Lab6.Models.Base;
using Lab6.Models.One_to_Many;

namespace Lab6.Models
{
    public class Student: BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }

        public ICollection<Model2>? Models2 { get; set; }
    }
}
