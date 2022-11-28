using AnimalCrossingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalCrossingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteSongEntriesController : ControllerBase
    {
        private readonly AnimalCrossingAPIDBContext _context;

        public FavoriteSongEntriesController(AnimalCrossingAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteSongs
        [HttpGet]
        public async Task<ActionResult<Response>> GetFavoriteSongs()
        {
            var response = new Response();
            response.statusCode = 200;
            response.statusDescription = "OK";
            response.favoriteSongsResult = await _context.FavoriteSongs.ToListAsync();
            return response;
        }

        // GET: api/FavoriteSongs/5
        [HttpGet("{VillagerName}")]
        public async Task<ActionResult<Response>> GetFavoriteSongs(string VillagerName)
        {
            var favoriteSong = await _context.FavoriteSongs.FindAsync(VillagerName);
            var response = new Response();
            if (favoriteSong == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Villager has no favorite song or invalid villager";
            }
            else
            {
                response.statusCode = 200;
                response.statusDescription = "OK";
                response.favoriteSongsResult.Add(favoriteSong);
            }
            return response;
        }

        // PUT: api/FavoriteSongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{VillagerName}")]
        public async Task<Response> PutFavoriteSongs(string VillagerName, FavoriteSongs favoriteSongs)
        {
            var response = new Response();

            response.statusCode = 200;
            response.statusDescription = "FAVORITE SONG UPDATED";

            if (VillagerName != favoriteSongs.VillagerName)
            {
                response.statusCode = 400;
                response.statusDescription = "BAD REQUEST";
            }

            _context.Entry(favoriteSongs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteSongsExists(VillagerName))
                {
                    response.statusCode = 404;
                    response.statusDescription = "VILLAGER NOT FOUND";
                }
                else
                {
                    throw;
                }
            }

            return response;
        }

        // POST: api/FavoriteSongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostFavoriteSongs(FavoriteSongs favoriteSongs)
        {
            var response = new Response();

            try
            {
                _context.FavoriteSongs.Add(favoriteSongs);
                await _context.SaveChangesAsync();
                response.statusCode = 201;
                response.statusDescription = "OK, VILLAGER'S FAVORITE SONG CREATED";
            }
            catch (DbUpdateException)
            {
                if (FavoriteSongsExists(favoriteSongs.VillagerName))
                {
                    response.statusCode = 409;
                    response.statusDescription = "VILLAGER'S FAVORITE SONG ALREADY EXISTS";
                }
                else
                {
                    response.statusCode = 404;
                    response.statusDescription = "VILLAGER'S FAVORITE SONG NOT FOUND";
                    throw;
                }
            }

            return response;
        }

        // DELETE: api/FavoriteSongs/5
        [HttpDelete("{VillagerName}")]
        public async Task<Response> DeleteFavoriteSongs(string VillagerName)
        {
            var favoriteSongs = await _context.FavoriteSongs.FindAsync(VillagerName);
            var response = new Response();

            response.statusCode = 200;
            response.statusDescription = "OK, VILLAGER'S FAVORITE SONG DELETED";

            if (favoriteSongs == null)
            {
                response.statusCode = 404;
                response.statusDescription = "VILLAGER'S FAVORITE SONG NOT FOUND";
            }

            _context.FavoriteSongs.Remove(favoriteSongs);
            await _context.SaveChangesAsync();

            return response;
        }

        private bool FavoriteSongsExists(string VillagerName)
        {
            return _context.FavoriteSongs.Any(e => e.VillagerName == VillagerName);
        }
    }
}
