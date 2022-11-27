using Microsoft.EntityFrameworkCore;
using AnimalCrossingApi.Models;

namespace AnimalCrossingApi.Models
{
    public class AnimalCrossingAPIDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AnimalCrossingAPIDBContext(DbContextOptions<AnimalCrossingAPIDBContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("AnimalCrossing");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Villager> Villagers { get; set; } = null!;

        public DbSet<Songs> Songs { get; set; } = null!;

        public DbSet<FavoriteSongs> FavoriteSongs { get; set; } = null!;
    }
}
