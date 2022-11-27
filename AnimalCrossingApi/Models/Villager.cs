using System.ComponentModel.DataAnnotations;

namespace AnimalCrossingApi.Models
{
    public class Villager
    {
        [Key]
        public string VillagerName { get; set; }
        public string Species { get; set; }
        public string Gender { get; set; }
        public string Personality { get; set; }
        public string Hobby { get; set; }
        public string Birthday { get; set; }
        public string Catchphrase { get; set; }
        public string Style1 { get; set; }
        public string Style2 { get; set; }
    }
}
