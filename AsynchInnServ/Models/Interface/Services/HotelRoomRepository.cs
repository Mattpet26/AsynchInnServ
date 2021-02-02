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

        /// <summary>
        /// Creates a new Room
        /// </summary>
        /// <param name="hotelRoom"></param>
        /// <param name="hotelId"></param>
        /// <returns></returns>
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

            return hotelRoom;
        }

        /// <summary>
        /// Gets a single HotelRoom
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <returns></returns>
        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            //first find a specific room for our new DTO
            //Do I need all these includes? We are looking for a specific room, so maybe?
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

        /// <summary>
        /// Gets a list of HotelRoom
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates a single HotelRoom
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <param name="hotelRoomDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes a HotelRoom
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <returns></returns>
        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom room = await _context.HotelRoom
                .FirstOrDefaultAsync(x => x.HotelId == hotelId && x.RoomNumber == roomNumber);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
