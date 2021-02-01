using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Api
{
    public class HotelRoomDTO
    {
        public int HotelID { get; set; }
        public int RoomNumber { get; set; }
        public int Rate { get; set; }
        public int RoomID { get; set; }
        public RoomDTO Room { get; set; }
    }
}
