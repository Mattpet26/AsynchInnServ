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
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;

        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        // GET: api/Hotels
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            return Ok(await _hotel.GetHotels());
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            //SELECT * FROM hotel WHERE id = {id}
            HotelDTO hotel = await _hotel.GetHotel(id);

            //Hotel hotel = await _hotel.GetHotel(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        [HttpPut("{id}")]
        [Authorize(Policy = "DistrictManager")]

        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            await _hotel.CreateHotel(hotel);
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // POST: api/Hotels
        [HttpPost]
        [Authorize(Policy = "DistrictManager")]

        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await _hotel.CreateHotel(hotel);
            return CreatedAtAction("GetHotels", new { id=hotel.Id}, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "DistrictManager")]

        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            await _hotel.DeleteHotel(id);
            return NoContent();
        }

    }
}
