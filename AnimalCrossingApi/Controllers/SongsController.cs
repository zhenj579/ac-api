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
        public async Task<Response> PutSongs(string name, Songs songs)
        {
            var response = new Response();

            response.statusCode = 200;
            response.statusDescription = "OK, SONG UPDATED";

            if (name != songs.Song)
            {
                response.statusCode = 400;
                response.statusDescription = "BAD REQUEST";
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
                    response.statusCode = 404;
                    response.statusDescription = "SONG NOT FOUND";
                }
                else
                {
                    throw;
                }
            }

            return response;
        }

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostSongs(Songs songs)
        {
            var response = new Response();

            try
            {
                _context.Songs.Add(songs);
                await _context.SaveChangesAsync();
                response.statusCode = 201;
                response.statusDescription = "OK, SONG CREATED";
            }
            catch (DbUpdateException)
            {
                if (SongsExists(songs.Song))
                {
                    response.statusCode = 409;
                    response.statusDescription = "SONG ALREADY EXISTS";
                }
                else
                {
                    response.statusCode = 404;
                    response.statusDescription = "SONG NOT FOUND";
                    throw;
                }
            }

            return response;
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteSongs(string song)
        {
            var songs = await _context.Songs.FindAsync(song);
            var response = new Response();

            response.statusCode = 200;
            response.statusDescription = "OK SONG DELETED";

            if (songs == null)
            {
                response.statusCode = 404;
                response.statusDescription = "SONG NOT FOUND";
                return response;
            }

            _context.Songs.Remove(songs);
            await _context.SaveChangesAsync();

            return response;
        }

        private bool SongsExists(string song)
        {
            return _context.Songs.Any(e => e.Song == song);
        }
    }
}
