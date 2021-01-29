using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models
{
    public class HotelRoom
    {
        //comp keys
        public int HotelId { get; set; }
        public int RoomId { get; set; }

        public int RoomNumber { get; set; }
        public int Rate { get; set; }

        public Room Room { get; set; }
        public Hotel Hotel { get; set; }
    }
}
