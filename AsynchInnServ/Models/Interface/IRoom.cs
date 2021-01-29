using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface
{
    public interface IRoom
    {
        /// <summary>
        /// Creates a new room for the hotel
        /// </summary>
        /// <param name="room">Room being added</param>
        /// <returns>created room</returns>
        Task<Room> CreateRoom(Room room);

        /// <summary>
        /// Gets all the rooms for the hotel
        /// </summary>
        /// <returns> list of rooms</returns>
        Task<List<Room>> GetRooms();

        /// <summary>
        /// Gets a single room for the hotel
        /// </summary>
        /// <param name="id">Gets unique id for the room</param>
        /// <returns>single room</returns>
        Task<Room> GetRoom(int id);

        /// <summary>
        /// Updates a room in the hotel
        /// </summary>
        /// <param name="room">Updates the unique room</param>
        /// <returns>updatedroom</returns>
        Task<Room> UpdateRoom(Room room);

        /// <summary>
        /// Deletes a room in the hotel
        /// </summary>
        /// <param name="id">deletes the room based off unique id</param>
        /// <returns>nothing</returns>
        Task DeleteRoom(int id);

        Task AddRoomAmmenities(int ammenityId, int roomId);

        Task RemoveRoomAmmenities(int ammenityId, int roomId);
    }
}
