using Backend.Models.Base;

namespace Backend.Models
{
    public class Stoc: BaseEntity
    {
        public int Nr_Produse { get; set; }
        public ICollection<StocProdus> StocProduse { get; set; }
    }
}
