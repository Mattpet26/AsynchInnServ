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
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await _room.GetRooms();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            Room room = await _room.GetRoom(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        [HttpPost("{roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult> AddRoomAmenityToRoom(int amenityId, int roomId)
        {
            await _room.AddRoomAmmenities(amenityId, roomId);
            return Ok();
        }

        [HttpDelete("{roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult> RemoveRoomAmenityFromRoom(int amenityId, int roomId)
        {
            await _room.RemoveRoomAmmenities(amenityId, roomId);
            return Ok();
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.RoomId)
            {
                return BadRequest();
            }

            var roomUpdate = await _room.UpdateRoom(room);
            return Ok(roomUpdate);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            await _room.CreateRoom(room);
            return CreatedAtAction("GetRooms", new { id=room.RoomId }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            await _room.DeleteRoom(id);
            return NoContent();
        }
    }
}
