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
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom context)
        {
            _hotelRoom = context;
        }

        // GET: api/HotelRooms
        [HttpGet("{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms(int hotelId)
        {
            return await _hotelRoom.GetHotelRooms(hotelId);
        }

        // GET: api/HotelRooms/5
        [HttpGet("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _hotelRoom.GetHotelRoom(hotelId, roomNumber);
            return hotelRoom;
        }

        // PUT: api/HotelRooms/5
        [HttpPut("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            if (hotelId != hotelRoom.HotelId || roomNumber != hotelRoom.RoomNumber)
            {
                return BadRequest();
            }
            await _hotelRoom.Update(hotelId, roomNumber, hotelRoom);
            return Ok();
        }

        // POST: api/HotelRooms
        [HttpPost("{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom, int hotelId)
        {
            await _hotelRoom.CreateRoom(hotelRoom, hotelId);
            return CreatedAtAction("GetHotelRoom", new { hotelRoom.HotelId, hotelRoom.RoomNumber }, hotelRoom);
        }

        //DELETE: api/HotelRooms/5
        [HttpDelete("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> Delete(int hotelId, int roomNumber)
        {
            await _hotelRoom.Delete(hotelId, roomNumber);
            return NoContent();
        }
    }
}
