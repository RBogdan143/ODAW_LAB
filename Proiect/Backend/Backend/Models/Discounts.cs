using Backend.Models.Base;

namespace Backend.Models
{
    public class Discounts: BaseEntity
    {
        public string Promo_Code { get; set; }
        public double Discount_Percent { get; set; }
        public Cos_Cumparaturi Cos_Redus { get; set; }
        public Guid Cos_CumparaturiId { get; set; }
    }
}
