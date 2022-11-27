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
        public async Task<ActionResult<IEnumerable<FavoriteSongEntries>>> GetFavoriteSongs()
        {
            return await _context.FavoriteSongEntries.ToListAsync();
        }

        // GET: api/FavoriteSongs/5
        [HttpGet("{VillagerName}")]
        public async Task<ActionResult<FavoriteSongEntries>> GetFavoriteSongs(string VillagerName)
        {
            var favoriteSongs = await _context.FavoriteSongEntries.FindAsync(VillagerName);

            if (favoriteSongs == null)
            {
                return NotFound();
            }

            return favoriteSongs;
        }

        // PUT: api/FavoriteSongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{VillagerName}")]
        public async Task<IActionResult> PutFavoriteSongs(string VillagerName, FavoriteSongEntries favoriteSongs)
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
        public async Task<ActionResult<FavoriteSongEntries>> PostFavoriteSongs(FavoriteSongEntries favoriteSongs)
        {
            _context.FavoriteSongEntries.Add(favoriteSongs);
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
            var favoriteSongs = await _context.FavoriteSongEntries.FindAsync(VillagerName);
            if (favoriteSongs == null)
            {
                return NotFound();
            }

            _context.FavoriteSongEntries.Remove(favoriteSongs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteSongsExists(string VillagerName)
        {
            return _context.FavoriteSongEntries.Any(e => e.VillagerName == VillagerName);
        }
    }
}
