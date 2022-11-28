using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalCrossingApi.Models;

namespace AnimalCrossingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillagersController : ControllerBase
    {
        private readonly AnimalCrossingAPIDBContext _context;

        public VillagersController(AnimalCrossingAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Villagers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Villager>>> GetVillagers()
        {
            return await _context.Villagers.ToListAsync();
        }

        // GET: api/Villagers/name
        [HttpGet("{name}")]
        public async Task<ActionResult<Response>> GetVillager(string name)
        {
            var villager = await _context.Villagers.FindAsync(name);
            var response = new Response();

            if (villager == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Invalid villager name or villager does not exist";
            }
            else
            {
                response.statusCode = 200;
                response.statusDescription = "OK";
                response.villagersResult.Add(villager);
            }

            return response;
        }

        // PUT: api/Villagers/Knox
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{VillagerName}")]
        public async Task<Response> PutVillager(string VillagerName, Villager villager)
        {
            var response = new Response();

            response.statusCode = 200;
            response.statusDescription = "OK, VILLAGER UPDATED";

            if (VillagerName != villager.VillagerName)
            {
                response.statusCode = 400;
                response.statusDescription = "BAD REQUEST";
            }

            _context.Entry(villager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VillagerExists(VillagerName))
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

        // POST: api/Villagers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostVillager(Villager villager)
        {
            var response = new Response();

            try
            {
                _context.Villagers.Add(villager);
                await _context.SaveChangesAsync();
                response.statusCode = 201;
                response.statusDescription = "OK, VILLAGER CREATED";

            }
            catch (DbUpdateException)
            {
                if (VillagerExists(villager.VillagerName))
                {
                    response.statusCode = 409;
                    response.statusDescription = "VILLAGER ALREADY EXISTS";
                }
                else
                {
                    response.statusCode = 404;
                    response.statusDescription = "VILLAGER NOT FOUND";
                    throw;
                }
            }

            return response;
        }

        // DELETE: api/Villagers/Knox
        [HttpDelete("{VillagerName}")]
        public async Task<Response> DeleteVillager(string VillagerName)
        {
            var villager = await _context.Villagers.FindAsync(VillagerName);
            var response = new Response();

            response.statusCode = 200;
            response.statusDescription = "OK, VILLAGER DELETED";

            if (villager == null)
            {
                response.statusCode = 404;
                response.statusDescription = "VILLAGER NOT FOUND";
                return response;
            }

            _context.Villagers.Remove(villager);
            await _context.SaveChangesAsync();

            return response;
        }

        private bool VillagerExists(string VillagerName)
        {
            return _context.Villagers.Any(e => e.VillagerName == VillagerName);
        }
    }
}
