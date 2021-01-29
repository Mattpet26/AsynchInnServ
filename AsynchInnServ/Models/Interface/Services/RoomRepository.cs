using AsynchInnServ.Data;
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
            RoomAmmenities ammenity = new RoomAmmenities()
            {
                AmmenityId = ammenityId,
                RoomId = roomId
            };
            _context.Entry(ammenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoomAmmenities(int ammenityId, int roomId)
        {
            var result = await _context.RoomAmmenities.FirstOrDefaultAsync(x => x.AmmenityId == ammenityId && x.RoomId == roomId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Room> CreateRoom(Room room)
        {
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task DeleteRoom(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetRoom(int id)
        {
            return await _context.Rooms
                .Include(x => x.HotelRoom)
                .ThenInclude(x => x.Hotel)
                .FirstOrDefaultAsync(x => x.RoomId == id);
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _context.Rooms
                .Include(x => x.HotelRoom)
                .ThenInclude(x => x.Hotel)
                .ToListAsync();
        }

        public async Task<Room> UpdateRoom(Room room)
        {
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }
    }
}
