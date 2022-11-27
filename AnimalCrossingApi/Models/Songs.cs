using System.ComponentModel.DataAnnotations;

namespace AnimalCrossingApi.Models
{
    public class Songs
    {
        [Key]
        public string Song { get; set; }
        public string BuyPrice { get; set; }
        public string SellPrice { get; set; }
        public string Size { get; set; }
        public string Src { get; set; }
        public string SrcNotes { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Catalog { get; set; }

    }
}
