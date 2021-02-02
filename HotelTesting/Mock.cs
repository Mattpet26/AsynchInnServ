using AsynchInnServ.Data;
using AsynchInnServ.Models;
using AsynchInnServ.Models.Interface.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelTesting
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AsynchInnDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            _db = new AsynchInnDbContext(
                new DbContextOptionsBuilder<AsynchInnDbContext>()
                .UseSqlite(_connection)
                .Options);
            _db.Database.EnsureCreated();
        }

        //Once we finish our tests, we dispose of everything.
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }

        protected Hotel CreateAndSaveTestHotel()
        {
            var hotel = new Hotel
            {
                City = "Toronto",
                Name = "Yopshi-Inn",
                State = "WA",
                PhoneNumber = 123456
            };
            return hotel;
        }
        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room
            {
                RoomId = 99,
                Layout = "mario",
                Name = "Superduper"
            };
            //add to db 
            _db.Rooms.Add(room);

            //now save it
            await _db.SaveChangesAsync();

            //now test it
            Assert.NotEqual(0, room.RoomId);
            return room;
        }
        protected async Task<Ammenities> CreateAndSaveTestAmmenity()
        {
            var amenity = new Ammenities
            {
                Id = 99,
                Name = "Yahoo"
            };
            //now test it
            Assert.NotEqual(0, amenity.Id);
            return amenity;
        }
    }
}
