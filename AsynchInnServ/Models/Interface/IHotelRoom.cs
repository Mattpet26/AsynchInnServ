using AsynchInnServ.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface
{
    public interface IHotelRoom
    {
        /// <summary>
        /// Adds a new HotelRoom
        /// </summary>
        /// <param name="hotelRoom">HotelRoom being added</param>
        /// <param name="hotelId">unique Hotel id</param>
        /// <returns>a created hotel room</returns>
        Task<HotelRoomDTO> CreateRoom(HotelRoomDTO hotelRoom, int hotelId);

        /// <summary>
        /// Gets all HotelRooms
        /// </summary>
        /// <param name="hotelId">the hotels unique id</param>
        /// <returns>a list of hotel rooms</returns>
        Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId);

        /// <summary>
        /// Gets a specific HotelRoom
        /// </summary>
        /// <param name="hotelId">unique hotel id</param>
        /// <param name="roomNumber">unique room number</param>
        /// <returns>a single hotel room</returns>
        Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber);

        /// <summary>
        /// Updates a specific HotelRoom
        /// </summary>
        /// <param name="hotelId">the hotels unique id</param>
        /// <param name="roomNumber">the rooms unique number</param>
        /// <param name="hotelRoom">hotel room being updated</param>
        /// <returns>nothing</returns>
        Task Update(int hotelId, int roomNumber, HotelRoomDTO hotelRoom);

        /// <summary>
        /// Removes a specific HotelRoom
        /// </summary>
        /// <param name="hotelId">the hotels unique id</param>
        /// <param name="roomNumber">room unique number</param>
        /// <returns>Nothing</returns>
        Task Delete(int hotelId, int roomNumber);
    }
}
