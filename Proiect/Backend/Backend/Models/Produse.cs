using Backend.Models.Base;

namespace Backend.Models
{
    public class Produse: BaseEntity
    {
        public string Nume { get; set; }
        public double Pret { get; set; }
        public string? Descriere { get; set; }
        public ICollection<StocProdus> StocProduse { get; set; }
        public Cos_Cumparaturi Cos { get; set; }
        public Guid Cos_CumparaturiId { get; set; }
    }
}
