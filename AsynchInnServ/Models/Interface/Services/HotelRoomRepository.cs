using AsynchInnServ.Data;
using AsynchInnServ.Models.Api;
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

        public async Task<HotelRoomDTO> CreateRoom(HotelRoomDTO hotelRoom, int hotelId)
        {
            HotelRoom room = new HotelRoom() 
            { 
                HotelId = hotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                RoomId = hotelRoom.RoomID
            };
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();
            room.RoomId = hotelId;

            return hotelRoom;
        }


        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            //first we find the room, then convert to a DTO, then return dto
            var hotelRoom = await _context.HotelRoom
                .Where(x => x.HotelId == hotelId && x.RoomId == roomNumber)
                .Include(x => x.Room)
                .ThenInclude(x => x.RoomAmmenities)
                .ThenInclude(x => x.Ammenities)
                .Include(x => x.Hotel)
                .FirstOrDefaultAsync();
            HotelRoomDTO DTOroom = new HotelRoomDTO()
            {
                RoomNumber = hotelRoom.RoomNumber,
                HotelID = hotelId,
                Rate = hotelRoom.Rate,
                RoomID = hotelRoom.RoomId,
                Room = await new RoomRepository(_context).GetRoom(hotelRoom.RoomId)
            };
            return DTOroom;
        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            var roomList = await _context.HotelRoom
                .Where(x => x.HotelId == hotelId)
                .Include(x => x.Room)
                .ToListAsync();
            var hotelRooms = new List<HotelRoomDTO>();

            foreach (var item in roomList)
            {
                hotelRooms.Add(await GetHotelRoom(item.HotelId, item.RoomNumber));
            }
            return hotelRooms;
        }

        public async Task Update(int hotelId, int roomNumber, HotelRoomDTO hotelRoomDTO)
        {
            var room = new HotelRoom()
            {
                HotelId = hotelId,
                RoomId = hotelRoomDTO.RoomID,
                RoomNumber = roomNumber,
                Rate = hotelRoomDTO.Rate
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
