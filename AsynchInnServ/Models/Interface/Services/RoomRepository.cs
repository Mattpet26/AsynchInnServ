using AsynchInnServ.Data;
using AsynchInnServ.Models.Api;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface.Services
{
    public class RoomRepository : IRoom
    {
        private AsynchInnDbContext _context;

        public RoomRepository(AsynchInnDbContext context)
        {
            _context = context;
        }

        public async Task AddRoomAmmenities(int ammenityId, int roomId)
        {
            RoomAmmenities roomAmenity = new RoomAmmenities()
            {
                AmmenityId = ammenityId,
                RoomId = roomId
            };

            _context.Entry(roomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoomAmmenities(int ammenityId, int roomId)
        {
            var result = await _context.RoomAmmenities.FirstOrDefaultAsync(x => x.AmmenityId == ammenityId && x.RoomId == roomId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<RoomDTO> CreateRoom(RoomDTO DTOroom)
        {
            Room room = new Room()
            {
                Name = DTOroom.Name,
                Layout = DTOroom.Layout
            };
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            DTOroom.ID = room.RoomId;
            return DTOroom;
        }

        public async Task DeleteRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<RoomDTO> GetRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            List<RoomAmmenities> roomAmenities = await _context.RoomAmmenities
                .Where(x => x.RoomId == id)
                .ToListAsync();
            List<AmmenitiesDTO> amenitiesList = new List<AmmenitiesDTO>();

            //foreach room, add to the dto list.
            foreach (var amenity in roomAmenities)
            {
                amenitiesList.Add(await new AmmenityRepository(_context).GetAmmenity(amenity.AmmenityId));
            }

            //create a dto room
            RoomDTO DTOroom = new RoomDTO()
            {
                ID = room.RoomId,
                Name = room.Name,
                Layout = room.Layout,
                Amenities = amenitiesList
            };
            return DTOroom;
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            var list = await _context.Rooms.ToListAsync();
            var roomList = new List<RoomDTO>();

            foreach (var room in list)
            {
                roomList.Add(await GetRoom(room.RoomId));
            }
            return roomList;
        }

        public async Task<RoomDTO> UpdateRoom(RoomDTO DTOroom)
        {
            Room room = new Room()
            {
                RoomId = DTOroom.ID,
                Name = DTOroom.Name,
                Layout = DTOroom.Layout
            };

            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return DTOroom;
        }
    }
}
