using System.ComponentModel.DataAnnotations;

namespace AnimalCrossingApi.Models
{
    public class FavoriteSongEntries
    {
        [Key]
        public string VillagerName { get; set; }
        public string Song { get; set; }
    }
}
