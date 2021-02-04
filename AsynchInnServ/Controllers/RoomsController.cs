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
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        // GET: api/Rooms
        [HttpGet]
        [Authorize(Policy = "DistrictManager")]
        [AllowAnonymous]
        public async Task<ActionResult<List<RoomDTO>>> GetRooms()
        {
            return await _room.GetRooms();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [Authorize(Policy = "DistrictManager")]
        [AllowAnonymous]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            RoomDTO room = await _room.GetRoom(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        [HttpPost("{roomId}/Amenity/{amenityId}")]
        [Authorize(Policy = "DistrictManager")]

        public async Task<ActionResult> AddRoomAmenityToRoom(int amenityId, int roomId)
        {
            await _room.AddRoomAmmenities(amenityId, roomId);
            return Ok();
        }

        [HttpDelete("{roomId}/Amenity/{amenityId}")]
        [Authorize(Policy = "DistrictManager")]

        public async Task<ActionResult> RemoveRoomAmenityFromRoom(int amenityId, int roomId)
        {
            await _room.RemoveRoomAmmenities(amenityId, roomId);
            return Ok();
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = "DistrictManager")]

        public async Task<IActionResult> PutRoom(int id, RoomDTO room)
        {
            if (id != room.ID)
            {
                return BadRequest();
            }

            await _room.UpdateRoom(room);
            return Ok();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = "Agent")]

        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO room)
        {
            await _room.CreateRoom(room);
            return CreatedAtAction("GetRoom", new { id=room.ID }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "DistrictManager")]

        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            await _room.DeleteRoom(id);
            return NoContent();
        }
    }
}
