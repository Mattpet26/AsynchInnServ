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

        /// <summary>
        /// Add a roomAmmenity
        /// </summary>
        /// <param name="ammenityId"></param>
        /// <param name="roomId"></param>
        /// <returns>nothing</returns>
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

        /// <summary>
        /// Remove a room Ammenity
        /// </summary>
        /// <param name="ammenityId"></param>
        /// <param name="roomId"></param>
        /// <returns>nothing</returns>
        public async Task RemoveRoomAmmenities(int ammenityId, int roomId)
        {
            var result = await _context.RoomAmmenities.FirstOrDefaultAsync(x => x.AmmenityId == ammenityId && x.RoomId == roomId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a new Room
        /// </summary>
        /// <param name="DTOroom"></param>
        /// <returns>newly created room</returns>
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

        /// <summary>
        /// Removes a room
        /// </summary>
        /// <param name="id"></param>
        /// <returns>nothing, deletes room</returns>
        public async Task DeleteRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a single room
        /// </summary>
        /// <param name="id"></param>
        /// <returns>roomdto</returns>
        public async Task<RoomDTO> GetRoom(int id)
        {
            //This is what john did in class, but I have no clue how to implement this way???

            //return await _context.Rooms
            //    .Select(room => new RoomDTO
            //    {
            //        ID = room.RoomId,
            //        Name = room.Name,
            //        Layout = room.Layout,
            //        Amenities = room.RoomAmmenities
            //        .Select(x => new AmmenitiesDTO
            //        { 
            //            RoomId = x.RoomId,
            //            AmmenityId = x.AmmenityId,
   
            //        }).ToList()
            //    }).FirstOrDefaultAsync(x => x.ID == id);
            Room room = await _context.Rooms.FindAsync(id);
            List<RoomAmmenities> roomAmenities = await _context.RoomAmmenities
                .Where(x => x.RoomId == id)
                //.Include(x => x.Ammenities)
                //.ThenInclude(x => x.RoomAmmenities)
                //.ThenInclude(x => x.AmmenityId)
                //.Include(x => x.Room)
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

        /// <summary>
        /// Gets the list of rooms
        /// </summary>
        /// <returns>list of roomdto</returns>
        public async Task<List<RoomDTO>> GetRooms()
        {
            var list = await _context.Rooms.ToListAsync();
            var roomList = new List<RoomDTO>();

            foreach (var room in list)
            {
                roomList.Add(await GetRoom(room.RoomId));
            }
            return roomList;

            //return await _context.RoomsDTO
            //    .Include(x => x.HotelRoom)
            //    .ThenInclude(x => x.RoomId)
            //    .Include(x => x.Layout)
            //    .Include(x => x.RoomAmmenities)
            //    .ToListAsync()
        }

        /// <summary>
        /// Updates a single room
        /// </summary>
        /// <param name="DTOroom"></param>
        /// <returns>updated roomdto</returns>
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
