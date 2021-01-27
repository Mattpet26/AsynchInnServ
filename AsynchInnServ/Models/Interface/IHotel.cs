using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface
{
    public interface IHotel
    {
        //CRUD
        Task<Hotel> CreateHotel(Hotel hotel);

        Task<Hotel> GetHotel(int id);

        Task<List<Hotel>> GetHotels();

        Task<Hotel> UpdateHotel(Hotel hotel);

        Task DeleteHotel(int id);
    }
}
