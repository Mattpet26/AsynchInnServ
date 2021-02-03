using AsynchInnServ.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Data
{
    //Change the inheritance, we not take from identityDbContext
    //This wraps our system, so everything knows what an AppUser is.
    public class AsynchInnDbContext : IdentityDbContext<AppUser>
    {
        public AsynchInnDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //We need identity to do it's pre-load stuff/work before we do.
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomAmmenities>().HasKey(x => new { x.AmmenityId, x.RoomId });
            modelBuilder.Entity<HotelRoom>().HasKey(x => new { x.HotelId, x.RoomNumber });

            modelBuilder.Entity<Hotel>().HasData(
              new Hotel { Name = "Yoshi-Inn", City = "ParadiseFalls", State="New-New-York", PhoneNumber = 1111111, Id = 1 },
              new Hotel { Name = "Chum-City-Inn", City = "Boonies", State = "Flaridah", PhoneNumber = 2222, Id = 2 },
              new Hotel { Name = "Third-Hotel", City = "Cold", State = "Alaska", PhoneNumber = 1, Id = 3 }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { Name = "King suite", RoomId = 1, Layout = "Green everywhere"},
                new Room { Name = "Jr suite", RoomId = 2, Layout = "For Widdle Babbies" },
                new Room { Name = "Normal suite", RoomId = 3, Layout = "Normal layout, idk man" }
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
        public DbSet<RoomAmmenities> RoomAmmenities { get; set; }
        public DbSet<HotelRoom> HotelRoom { get; set; }
        public object RoomDTO { get; internal set; }
    }
}
