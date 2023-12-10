using Lab6.Models.Base;

namespace Lab6.Models.One_to_One
{
    public class Model5: BaseEntity
    {
        public string? Name { get; set; }

        // relation
        public Model6 Model6 { get; set; }
    }
}
