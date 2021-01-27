using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface
{
    public interface IAmmenities
    {
        Task<Ammenities> CreateAmmenity(Ammenities ammenities);

        Task<List<Ammenities>> GetAmmenities();

        Task<Ammenities> GetAmmenity(int id);

        Task<Ammenities> UpdateAmmenities(Ammenities ammenities);

        Task DeleteAmmenity(int id);
    }
}
