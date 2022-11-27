namespace AnimalCrossingApi.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public List<FavoriteSongs> favoriteSongsResult = new();
        public List<Songs> songsResult { get; set; } = new();
        public List<Villager> villagersResult { get; set; } = new();

    }
}
