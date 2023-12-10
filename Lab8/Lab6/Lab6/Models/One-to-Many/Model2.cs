using Lab6.Models.Base;

namespace Lab6.Models.One_to_Many
{
    public class Model2: BaseEntity
    {
        public string Name { get; set; }

        // relation
        public Model1 Model1 { get; set; }

        public Guid Model1Id { get; set; }
    }
}
