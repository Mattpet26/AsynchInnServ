using AsynchInnServ.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface
{
    public interface IHotel
    {
        /// <summary>
        /// Creates a new hotel
        /// </summary>
        /// <param name="hotel">Unique hotel created</param>
        /// <returns>created hotel </returns>
        Task<Hotel> CreateHotel(Hotel hotel);

        /// <summary>
        /// Gets a single hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns>single hotel</returns>
        Task<HotelDTO> GetHotel(int id);               //changed to DTO

        /// <summary>
        /// Gets a list of all hotels
        /// </summary>
        /// <returns>list of hotels</returns>
        Task<List<HotelDTO>> GetHotels();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotel">updates a unique hotel</param>
        /// <returns>updated hotel</returns>
        Task UpdateHotel(Hotel hotel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">deletes a unique hotel based off id</param>
        /// <returns>nothing</returns>
        Task DeleteHotel(int id);
    }
}
