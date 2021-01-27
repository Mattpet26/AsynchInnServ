using AsynchInnServ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Data
{
    public class AsynchInnDbContext : DbContext
    {
        public AsynchInnDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //seeding
            modelBuilder.Entity<Hotel>().HasData(
              new Hotel { Name = "Yoshi-Inn", City = "ParadiseFalls", State="New-New-York", PhoneNumber = 1111111, Id = 1 },
              new Hotel { Name = "Chum-City-Inn", City = "Boonies", State = "Flaridah", PhoneNumber = 2222, Id = 2 },
              new Hotel { Name = "Third-Hotel", City = "Cold", State = "Alaska", PhoneNumber = 1, Id = 3 }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { Name = "King suite", Id = 1, Layout = "Green everywhere"},
                new Room { Name = "Jr suite", Id = 2, Layout = "For Widdle Babbies" },
                new Room { Name = "Normal suite", Id = 3, Layout = "Normal layout, idk man" }
                );

            modelBuilder.Entity<Ammenities>().HasData(
                new Ammenities { Name = "Quadruple Fridge", Id = 1},
                new Ammenities { Name = "Mini-Bed", Id = 2},
                new Ammenities { Name = "MEGA PACKAGE", Id = 3 }
                );
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Ammenities> Ammenities { get; set; }
    }
}
