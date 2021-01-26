using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models
{
    public class Ammenities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoomAmmenities> RoomAmenities { get; set; }
    }
}
