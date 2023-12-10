//Temă 1
using Lab6.Models.Base;
using System.Text.Json.Serialization;

namespace Lab6.Models.One_to_Many
{
    public class Manual: BaseEntity
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public Teacher Teachers { get; set; }

        public Guid TeacherId { get; set; }
    }
}
