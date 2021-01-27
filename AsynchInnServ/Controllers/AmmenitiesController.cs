using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsynchInnServ.Data;
using AsynchInnServ.Models;

namespace AsynchInnServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmmenitiesController : ControllerBase
    {
        private readonly AsynchInnDbContext _context;

        public AmmenitiesController(AsynchInnDbContext context)
        {
            _context = context;
        }

        // GET: api/Ammenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ammenities>>> GetAmmenities()
        {
            return await _context.Ammenities.ToListAsync();
        }

        // GET: api/Ammenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ammenities>> GetAmmenities(int id)
        {
            var ammenities = await _context.Ammenities.FindAsync(id);

            if (ammenities == null)
            {
                return NotFound();
            }

            return ammenities;
        }

        // PUT: api/Ammenities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmmenities(int id, Ammenities ammenities)
        {
            if (id != ammenities.Id)
            {
                return BadRequest();
            }

            _context.Entry(ammenities).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmmenitiesExists(id))
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

        // POST: api/Ammenities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ammenities>> PostAmmenities(Ammenities ammenities)
        {
            _context.Ammenities.Add(ammenities);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmmenities", new { id = ammenities.Id }, ammenities);
        }

        // DELETE: api/Ammenities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ammenities>> DeleteAmmenities(int id)
        {
            var ammenities = await _context.Ammenities.FindAsync(id);
            if (ammenities == null)
            {
                return NotFound();
            }

            _context.Ammenities.Remove(ammenities);
            await _context.SaveChangesAsync();

            return ammenities;
        }

        private bool AmmenitiesExists(int id)
        {
            return _context.Ammenities.Any(e => e.Id == id);
        }
    }
}
