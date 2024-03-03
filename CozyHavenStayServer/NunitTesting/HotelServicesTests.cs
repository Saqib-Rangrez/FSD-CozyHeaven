using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NunitTesting
{
    [TestFixture]
    public class HotelServicesTests
    {
        private Mock<ILogger<HotelServices>> _loggerMock;
        private Mock<IRepository<Hotel>> _hotelRepositoryMock;
        private Mock<IRepository<HotelImage>> _hotelImageRepositoryMock;
        private IHotelServices _hotelServices;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<HotelServices>>();
            _hotelRepositoryMock = new Mock<IRepository<Hotel>>();
            _hotelImageRepositoryMock = new Mock<IRepository<HotelImage>>();
            _hotelServices = new HotelServices(_loggerMock.Object, _hotelRepositoryMock.Object, _hotelImageRepositoryMock.Object);
        }


        [Test]
        public async Task GetAllHotelsAsync_ReturnsListOfHotels_WhenHotelsExist()
        {
            // Arrange
            var hotels = new List<Hotel>
            {
                new Hotel { HotelId = 1, Name = "Hotel A" },
                new Hotel { HotelId = 2, Name = "Hotel B" }
            };
            _hotelRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(hotels);

            // Act
            var result = await _hotelServices.GetAllHotelsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hotels.Count, result.Count);
            Assert.IsTrue(result.All(h => hotels.Any(hotel => hotel.HotelId == h.HotelId && hotel.Name == h.Name)));
        }

        [Test]
        public async Task GetAllHotelsAsync_ReturnsNull_WhenNoHotelsExist()
        {
            // Arrange
            _hotelRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync((List<Hotel>)null);

            // Act
            var result = await _hotelServices.GetAllHotelsAsync();

            // Assert
            Assert.IsNull(result);
        }
        [Test]
        public async Task GetHotelByIdAsync_ReturnsHotel_WhenHotelExists()
        {
            // Arrange
            int hotelId = 1;
            var hotel = new Hotel { HotelId = hotelId, Name = "Hotel A" };
            _hotelRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), false)).ReturnsAsync(hotel);

            // Act
            var result = await _hotelServices.GetHotelByIdAsync(hotelId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hotelId, result.HotelId);
            Assert.AreEqual(hotel.Name, result.Name);
        }


        [Test]
        public async Task GetHotelByIdAsync_ReturnsNull_WhenHotelDoesNotExist()
        {
            // Arrange
            int hotelId = 1;
            _hotelRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), false)).ReturnsAsync((Hotel)null);

            // Act
            var result = await _hotelServices.GetHotelByIdAsync(hotelId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateHotelAsync_ReturnsCreatedHotel_WhenValidHotelProvided()
        {
            // Arrange
            var hotel = new Hotel { Name = "Hotel C", Location = "Location C", Description = "Description C", Amenities = "Amenities C" };
            _hotelRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Hotel>())).ReturnsAsync(hotel);

            // Act
            var result = await _hotelServices.CreateHotelAsync(hotel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hotel.Name, result.Name);
            Assert.AreEqual(hotel.Location, result.Location);
            Assert.AreEqual(hotel.Description, result.Description);
            Assert.AreEqual(hotel.Amenities, result.Amenities);
        }

        [Test]
        public async Task CreateHotelAsync_ReturnsNull_WhenInvalidHotelProvided()
        {
            // Arrange
            Hotel hotel = null;

            // Act
            var result = await _hotelServices.CreateHotelAsync(hotel);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateHotelAsync_ReturnsTrue_WhenHotelUpdatedSuccessfully()
        {
            // Arrange
            var hotelId = 1;
            var hotel = new Hotel { HotelId = hotelId, Name = "Updated Hotel Name" };
            _hotelRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), true)).ReturnsAsync(hotel);

            // Act
            var result = await _hotelServices.UpdateHotelAsync(hotel);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateHotelAsync_ReturnsFalse_WhenHotelToUpdateDoesNotExist()
        {
            // Arrange
            var hotelId = 1;
            var hotel = new Hotel { HotelId = hotelId, Name = "Updated Hotel Name" };
            _hotelRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), true)).ReturnsAsync((Hotel)null);

            // Act
            var result = await _hotelServices.UpdateHotelAsync(hotel);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteHotelAsync_ReturnsTrue_WhenHotelDeletedSuccessfully()
        {
            // Arrange
            var hotelId = 1;
            var hotel = new Hotel { HotelId = hotelId };
            _hotelRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), false)).ReturnsAsync(hotel);

            // Act
            var result = await _hotelServices.DeleteHotelAsync(hotelId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteHotelAsync_ReturnsFalse_WhenHotelToDeleteDoesNotExist()
        {
            // Arrange
            var hotelId = 1;
            _hotelRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), false)).ReturnsAsync((Hotel)null);

            // Act
            var result = await _hotelServices.DeleteHotelAsync(hotelId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
