using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models
{
    public class RoomAmmenities
    {
        public int AmmenitiesId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public Ammenities Ammenities { get; set; }
    }
}
