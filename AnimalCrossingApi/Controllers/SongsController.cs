using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalCrossingApi.Models;

namespace AnimalCrossingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly AnimalCrossingAPIDBContext _context;

        public SongsController(AnimalCrossingAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Songs>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        // GET: api/Songs/Agent K.K.
        [HttpGet("{name}")]
        public async Task<ActionResult<Response>> GetSongs(string name)
        {
            var songs = await _context.Songs.FindAsync(name);
            var response = new Response();

            if (songs == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Invalid song name or song does not exist";
            }
            else
            {
                response.statusCode = 200;
                response.statusDescription = "OK";
                response.songsResult.Add(songs);
            }
            return response;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> PutSongs(string name, Songs songs)
        {
            if (name != songs.Song)
            {
                return BadRequest();
            }

            _context.Entry(songs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongsExists(name))
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

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Songs>> PostSongs(Songs songs)
        {
            _context.Songs.Add(songs);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SongsExists(songs.Song))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSongs", new { song = songs.Song }, songs);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSongs(string song)
        {
            var songs = await _context.Songs.FindAsync(song);
            if (songs == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(songs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongsExists(string song)
        {
            return _context.Songs.Any(e => e.Song == song);
        }
    }
}
