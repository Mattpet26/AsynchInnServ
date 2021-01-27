using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface
{
    public interface IRoom
    {
        Task<Room> CreateRoom(Room room);

        Task<List<Room>> GetRooms();

        Task<Room> GetRoom(int id);

        Task<Room> UpdateRoom(Room room);

        Task DeleteRoom(int id);

        //Task AddRoomAmmenities(int ammenityId, int roomId);

        //Task RemoveRoomAmmenities(int ammenityId, int roomId);
    }
}
