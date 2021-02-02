using AsynchInnServ.Models;
using AsynchInnServ.Models.Api;
using AsynchInnServ.Models.Interface.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HotelTesting
{
    public class UnitTest1 : Mock
    {
        [Fact]
        public async Task CanCreateAndSaveHotel()
        {
            var hotel = CreateAndSaveTestHotel();

            var repository = new HotelRepository(_db);

            await repository.CreateHotel(hotel);

            var actualHotel = await repository.GetHotel(hotel.Id);
            Assert.Equal(actualHotel.Id, hotel.Id);
            Assert.NotNull(hotel);
            Assert.Equal(typeof(Hotel), hotel.GetType());
        }
        [Fact]
        public async Task CanCreateAndSaveAmmenity()
        {
            var repository = new AmmenityRepository(_db);

            var amenity = await CreateAndSaveTestAmmenity();
            var actualAmmenity = await repository.GetAmmenity(amenity.Id);

            Assert.NotNull(amenity);
            Assert.Equal(actualAmmenity.Id, amenity.Id);
            Assert.Equal(typeof(Ammenities), amenity.GetType());
        }
    }
}
