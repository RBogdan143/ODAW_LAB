using Lab6.Models.Base;

namespace Lab6.Models.One_to_One
{
    public class Model6: BaseEntity
    {
        public string? Name { get; set; }

        // relations
        public Model5 Model5 { get; set; }
        public Guid Model5Id { get; set; }  
    }
}
