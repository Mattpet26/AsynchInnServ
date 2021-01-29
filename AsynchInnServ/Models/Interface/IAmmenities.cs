using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface
{
    public interface IAmmenities
    {
        /// <summary>
        /// Creates a new unique ammenity
        /// </summary>
        /// <param name="ammenities">unique new ammenity</param>
        /// <returns>created amenity</returns>
        Task<Ammenities> CreateAmmenity(Ammenities ammenities);

        /// <summary>
        /// gets a list of ammenities
        /// </summary>
        /// <returns>list of amenities</returns>
        Task<List<Ammenities>> GetAmmenities();

        /// <summary>
        /// Gets a unique ammenity
        /// </summary>
        /// <param name="id">ammenity unique id</param>
        /// <returns>single amenity</returns>
        Task<Ammenities> GetAmmenity(int id);

        /// <summary>
        /// updates an ammenity
        /// </summary>
        /// <param name="ammenities">Unique ammenity being updated</param>
        /// <returns>updated amenity</returns>
        Task<Ammenities> UpdateAmmenities(Ammenities ammenities);

        /// <summary>
        /// deletes an ammenity
        /// </summary>
        /// <param name="id">deletes ammenity based on id</param>
        /// <returns>nothing</returns>
        Task DeleteAmmenity(int id);
    }
}
