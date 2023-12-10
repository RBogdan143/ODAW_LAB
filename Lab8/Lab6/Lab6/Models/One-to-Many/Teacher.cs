//Temă 2
using Lab6.Models.Base;
using System.Text.Json.Serialization;

namespace Lab6.Models.One_to_Many
{
    public class Teacher: BaseEntity
    {
        public string? Name { get; set; }
        public double? Salary { get; set; }
        public ICollection<Manual>? Manuals { get; set; }
    }
}
