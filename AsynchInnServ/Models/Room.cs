﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }
        public List<Ammenities> Ammenities { get; set; }
        public List<HotelRoom> HotelRoom { get; set; }
    }
}
