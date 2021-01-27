using AsynchInnServ.Data;
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
        public async Task<Ammenities> CreateAmmenity(Ammenities ammenities)
        {
            _context.Entry(ammenities).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();
            return ammenities;
        }

        /// <summary>
        /// Delete an amenity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAmmenity(int id)
        {
            Ammenities ammenities = await GetAmmenity(id);
            _context.Entry(ammenities).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all the amenities
        /// </summary>
        /// <returns></returns>
        public async Task<List<Ammenities>> GetAmmenities()
        {
            var ammenity = await _context.Ammenities.ToListAsync();
            return ammenity;
        }

        /// <summary>
        /// Get a single amenity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Ammenities> GetAmmenity(int id)
        {
            Ammenities ammenities = await _context.Ammenities.FindAsync(id);
            return ammenities;
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
