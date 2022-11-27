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
        public async Task<ActionResult<Villager>> GetVillager(string name)
        {
            var villager = await _context.Villagers.FindAsync(name);

            if (villager == null)
            {
                return NotFound();
            }

            return villager;
        }

        // PUT: api/Villagers/Knox
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVillager(string VillagerName, Villager villager)
        {
            if (VillagerName != villager.VillagerName)
            {
                return BadRequest();
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Villagers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Villager>> PostVillager(Villager villager)
        {
            _context.Villagers.Add(villager);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VillagerExists(villager.VillagerName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVillager", new { VillagerName = villager.VillagerName }, villager);
        }

        // DELETE: api/Villagers/Knox
        [HttpDelete("{VillagerName}")]
        public async Task<IActionResult> DeleteVillager(string VillagerName)
        {
            var villager = await _context.Villagers.FindAsync(VillagerName);
            if (villager == null)
            {
                return NotFound();
            }

            _context.Villagers.Remove(villager);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VillagerExists(string VillagerName)
        {
            return _context.Villagers.Any(e => e.VillagerName == VillagerName);
        }
    }
}
