using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models
{
    public class RoomAmmenities
    {
        //nav properties
        public Ammenities Ammenities { get; set; }
        public Room Room { get; set; }

        //comp keys
        public int RoomId { get; set; }
        public int AmmenityId { get; set; }
    }
}
