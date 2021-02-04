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
using AsynchInnServ.Models.Api;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Policy = "PropertyManager")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AmmenitiesDTO>>> GetAmmenities()
        {
            return Ok(await _ammenities.GetAmmenities());
        }

        // GET: api/Ammenities/5
        [HttpGet("{id}")]
        [Authorize(Policy = "PropertyManager")]
        [AllowAnonymous]

        public async Task<ActionResult<AmmenitiesDTO>> GetAmmenity(int id)
        {
            AmmenitiesDTO ammenity = await _ammenities.GetAmmenity(id);

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
        [Authorize(Policy = "PropertyManager")]

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
        [HttpPost]
        [Authorize(Policy = "PropertyManager")]

        public async Task<ActionResult<AmmenitiesDTO>> PostAmmenities(AmmenitiesDTO ammenities)
        {
            await _ammenities.CreateAmmenity(ammenities);
            return CreatedAtAction("GetAmmenities", new { id=ammenities.Id}, ammenities);
        }

        // DELETE: api/Ammenities/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "DistrictManager")]

        public async Task<ActionResult<Ammenities>> DeleteAmmenities(int id)
        {
            await _ammenities.DeleteAmmenity(id);
            return NoContent();
        }
    }
}
