using AsynchInnServ.Data;
using AsynchInnServ.Models.Api;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface.Services
{
    public class HotelRepository : IHotel
    {
        private AsynchInnDbContext _context;

        public HotelRepository(AsynchInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            _context.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }

        /// <summary>
        /// Deletes a single hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteHotel(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a single hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HotelDTO> GetHotel(int id)     //Changed to DTO
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);

            //convert the hotel to a DTO, then return it
            HotelDTO DTOhotel = new HotelDTO()
            {
                Id = id,
                City = hotel.City,
                State = hotel.State,
                Name = hotel.Name,
                HotelRooms = await new HotelRoomRepository(_context).GetHotelRooms(hotel.Id)
            };
            return DTOhotel;
            
        }

        /// <summary>
        /// Gets a list of hotels
        /// </summary>
        /// <returns></returns>
        public async Task<List<HotelDTO>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            var hotelList = new List<HotelDTO>();

            foreach (var hotel in hotels)
            {
                hotelList.Add(await GetHotel(hotel.Id));
            }
            return hotelList;
        }

        /// <summary>
        /// Updates a hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        public async Task UpdateHotel(Hotel hotel)
        {
            _context.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
