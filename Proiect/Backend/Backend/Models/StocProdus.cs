namespace Backend.Models
{
    public class StocProdus
    {
        public Guid StocId { get; set; }
        public Stoc Stoc { get; set; }

        public Guid ProdusId { get; set; }
        public Produse Produs { get; set; }
    }
}
