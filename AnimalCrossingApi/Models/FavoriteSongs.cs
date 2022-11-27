using System.ComponentModel.DataAnnotations;

namespace AnimalCrossingApi.Models
{
    public class FavoriteSongs
    {
        [Key]
        public string VillagerName { get; set; }
        public string Song { get; set; }
    }
}
