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
        public async Task<ActionResult<IEnumerable<FavoriteSongs>>> GetFavoriteSongs()
        {
            return await _context.FavoriteSongs.ToListAsync();
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteSongs(string VillagerName, FavoriteSongs favoriteSongs)
        {
            if (VillagerName != favoriteSongs.VillagerName)
            {
                return BadRequest();
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FavoriteSongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavoriteSongs>> PostFavoriteSongs(FavoriteSongs favoriteSongs)
        {
            _context.FavoriteSongs.Add(favoriteSongs);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FavoriteSongsExists(favoriteSongs.VillagerName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFavoriteSongs", new { VillagerName = favoriteSongs.VillagerName }, favoriteSongs);
        }

        // DELETE: api/FavoriteSongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteSongs(string VillagerName)
        {
            var favoriteSongs = await _context.FavoriteSongs.FindAsync(VillagerName);
            if (favoriteSongs == null)
            {
                return NotFound();
            }

            _context.FavoriteSongs.Remove(favoriteSongs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteSongsExists(string VillagerName)
        {
            return _context.FavoriteSongs.Any(e => e.VillagerName == VillagerName);
        }
    }
}
