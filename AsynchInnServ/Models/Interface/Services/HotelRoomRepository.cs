using AsynchInnServ.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private readonly AsynchInnDbContext _context;

        public HotelRoomRepository(AsynchInnDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoom> CreateRoom(HotelRoom hotelRoom, int hotelId)
        {
            HotelRoom room = new HotelRoom() { HotelId = hotelId, RoomNumber = hotelRoom.RoomNumber, Rate = hotelRoom.Rate, RoomId = hotelRoom.RoomId };
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }


        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRoom
                .Where(x => x.RoomNumber == roomNumber && x.HotelId == hotelId)
                .FirstOrDefaultAsync();

            HotelRoom room = new HotelRoom() {RoomNumber = roomNumber, HotelId = hotelId };
            return room;
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            var list = await _context.HotelRoom
                .Where(x => x.HotelId == hotelId)
                .ToListAsync();
            var listRooms = new List<HotelRoom>();

            foreach (var room in list)
            {
                //we need to call the method to get one room, but then add them to the list
                listRooms.Add(await GetHotelRoom(room.HotelId, room.RoomNumber));
            }
            return listRooms;
        }

        public async Task Update(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            var room = new HotelRoom()
            {
                HotelId = hotelId,
                RoomId = hotelRoom.RoomId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate
            };
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom room = await _context.HotelRoom
                                .FirstOrDefaultAsync(x => x.HotelId == hotelId && x.RoomNumber == roomNumber);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
