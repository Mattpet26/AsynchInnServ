using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models
{
    public class Room
    {
        //comp key
        public int RoomId { get; set; }

        public string Name { get; set; }
        public string Layout { get; set; }

        //navigation properties
        public List<RoomAmmenities> RoomAmmenities { get; set; }
        public List<HotelRoom> HotelRoom { get; set; }
    }
}
