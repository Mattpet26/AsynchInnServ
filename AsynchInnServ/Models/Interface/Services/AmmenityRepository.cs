using AsynchInnServ.Data;
using AsynchInnServ.Models.Api;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface.Services
{
    public class AmmenityRepository : IAmmenities
    {
        private AsynchInnDbContext _context;

        public AmmenityRepository(AsynchInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a new amenity
        /// </summary>
        /// <param name="ammenities"></param>
        /// <returns></returns>
        public async Task<AmmenitiesDTO> CreateAmmenity(AmmenitiesDTO ammenities)
        {
            Ammenities amenity = new Ammenities()
            {
                Name = ammenities.Name,
                Id = ammenities.Id
            };
            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            ammenities.Id = amenity.Id;
            return ammenities;
        }

        /// <summary>
        /// Delete an amenity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAmmenity(int id)
        {
            Ammenities ammenities = await _context.Ammenities.FindAsync(id);
            _context.Entry(ammenities).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all the amenities
        /// </summary>
        /// <returns></returns>
        public async Task<List<AmmenitiesDTO>> GetAmmenities()
        {
            var ammenity = await _context.Ammenities.ToListAsync();
            var ammenitieslist = new List<AmmenitiesDTO>();
            foreach (var item in ammenity)
            {
                ammenitieslist.Add(await GetAmmenity(item.Id));
            }
            return ammenitieslist;
        }

        /// <summary>
        /// Get a single amenity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AmmenitiesDTO> GetAmmenity(int id)
        {
            Ammenities amenity = await _context.Ammenities.FindAsync(id);
            AmmenitiesDTO DTOAmmenity = new AmmenitiesDTO()
            {
                Id = amenity.Id,
                Name = amenity.Name
            };
            return DTOAmmenity;
        }

        /// <summary>
        /// Update a single amenity
        /// </summary>
        /// <param name="ammenities"></param>
        /// <returns></returns>
        public async Task<Ammenities> UpdateAmmenities(Ammenities ammenities)
        {
            _context.Entry(ammenities).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return ammenities;
        }
    }
}
