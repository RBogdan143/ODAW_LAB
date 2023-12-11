using Backend.Models.Base;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class Stoc: BaseEntity
    {
        public int Nr_Produse { get; set; }
        [JsonIgnore]
        public ICollection<StocProdus>? StocProduse { get; set; }
    }
}
