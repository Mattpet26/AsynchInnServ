using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsynchInnServ.Data;
using AsynchInnServ.Models;
using AsynchInnServ.Models.Interface;

namespace AsynchInnServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmmenitiesController : ControllerBase
    {
        private readonly IAmmenities _ammenities;

        public AmmenitiesController(IAmmenities ammenities)
        {
            _ammenities = ammenities;
        }

        // GET: api/Ammenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ammenities>>> GetAmmenities()
        {
            return Ok(await _ammenities.GetAmmenities());
        }

        // GET: api/Ammenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ammenities>> GetAmmenity(int id)
        {
            Ammenities ammenity = await _ammenities.GetAmmenity(id);

            if (ammenity == null)
            {
                return NotFound();
            }

            return ammenity;
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

            var updateAmmenity = await _ammenities.UpdateAmmenities(ammenities);
            return Ok(updateAmmenity);
        }

        // POST: api/Ammenities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ammenities>> PostAmmenities(Ammenities ammenities)
        {
            await _ammenities.CreateAmmenity(ammenities);
            return CreatedAtAction("GetAmmenities", new { id=ammenities.Id}, ammenities);
        }

        // DELETE: api/Ammenities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ammenities>> DeleteAmmenities(int id)
        {
            await _ammenities.DeleteAmmenity(id);
            return NoContent();
        }
    }
}
