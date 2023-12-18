using Backend.Models.Base;

namespace Backend.Models
{
    public class Cos_Cumparaturi: BaseEntity
    {
        public ICollection<Produse> Produse_Alese { get; set; }
        public Discounts Discount { get; set; }
        public double Total_Plata
        {
            get
            {
                // Calculăm suma totală a produselor din coș
                double subtotal = Produse_Alese.Sum(p => p.Pret);

                // Aplicăm discount-ul dacă există
                if (Discount != null)
                {
                    subtotal -= subtotal * (Discount.Discount_Percent / 100);
                }

                return subtotal;
            }
        }
        public User user { get; set; }
        public Guid UserId { get; set; }
    }
}
