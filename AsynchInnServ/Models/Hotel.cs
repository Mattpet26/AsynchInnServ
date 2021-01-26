using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PhoneNumber { get; set; }
        public List<HotelRoom> HotelRoom { get; set; }
    }
}
