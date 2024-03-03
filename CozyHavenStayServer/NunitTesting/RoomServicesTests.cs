using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
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
    public class RoomServicesTests
    {
        private Mock<IRepository<Room>> _roomRepositoryMock;
        private Mock<IRepository<RoomImage>> _roomImageRepositoryMock;
        private ILogger<RoomServices> _logger;
        private RoomServices _roomServices;

        [SetUp]
        public void Setup()
        {
            _roomRepositoryMock = new Mock<IRepository<Room>>();
            _roomImageRepositoryMock = new Mock<IRepository<RoomImage>>();
            _logger = new Mock<ILogger<RoomServices>>().Object;
            _roomServices = new RoomServices(_logger, _roomRepositoryMock.Object, _roomImageRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllRoomsAsync_ReturnsListOfRooms()
        {
            // Arrange
            var expectedRooms = new List<Room> { new Room(), new Room() };
            _roomRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedRooms);

            // Act
            var result = await _roomServices.GetAllRoomsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRooms.Count, result.Count);
        }

        [Test]
        public async Task GetRoomByIdAsync_ReturnsRoom_WhenRoomExists()
        {
            // Arrange
            int roomId = 1;
            var room = new Room { RoomId = roomId };
            _roomRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Room, bool>>>(), false)).ReturnsAsync(room);

            // Act
            var result = await _roomServices.GetRoomByIdAsync(roomId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(roomId, result.RoomId);
        }

        [Test]
        public async Task GetRoomByIdAsync_ReturnsNull_WhenRoomDoesNotExist()
        {
            // Arrange
            int roomId = 1;
            _roomRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Room, bool>>>(), false)).ReturnsAsync((Room)null);

            // Act
            var result = await _roomServices.GetRoomByIdAsync(roomId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateRoomAsync_ReturnsCreatedRoom_WhenValidRoomProvided()
        {
            // Arrange
            var room = new Room { RoomId = 1, RoomType = "Standard", MaxOccupancy = 2 };
            _roomRepositoryMock.Setup(repo => repo.CreateAsync(room)).ReturnsAsync(room);

            // Act
            var result = await _roomServices.CreateRoomAsync(room);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(room, result);
        }

        [Test]
        public async Task UpdateRoomAsync_ReturnsTrue_WhenRoomUpdatedSuccessfully()
        {
            // Arrange
            var room = new Room { RoomId = 1, RoomType = "Standard", MaxOccupancy = 2 };
            _roomRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Room, bool>>>(), true)).ReturnsAsync(room);

            // Act
            var result = await _roomServices.UpdateRoomAsync(room);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateRoomAsync_ReturnsFalse_WhenRoomToUpdateDoesNotExist()
        {
            // Arrange
            var room = new Room { RoomId = 1, RoomType = "Standard", MaxOccupancy = 2 };
            _roomRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Room, bool>>>(), true)).ReturnsAsync((Room)null);

            // Act
            var result = await _roomServices.UpdateRoomAsync(room);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteRoomAsync_ReturnsTrue_WhenRoomDeletedSuccessfully()
        {
            // Arrange
            var roomId = 1;
            var room = new Room { RoomId = roomId };
            _roomRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Room, bool>>>(), false)).ReturnsAsync(room);

            // Act
            var result = await _roomServices.DeleteRoomAsync(roomId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteRoomAsync_ReturnsFalse_WhenRoomToDeleteDoesNotExist()
        {
            // Arrange
            var roomId = 1;
            _roomRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Room, bool>>>(), false)).ReturnsAsync((Room)null);

            // Act
            var result = await _roomServices.DeleteRoomAsync(roomId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddRoomImageAsync_ReturnsCreatedImage_WhenValidImageProvided()
        {
            // Arrange
            var roomImage = new RoomImage { RoomId = 1, ImageUrl = "image-url.jpg" };
            _roomImageRepositoryMock.Setup(repo => repo.CreateAsync(roomImage)).ReturnsAsync(roomImage);

            // Act
            var result = await _roomServices.AddRoomImageAsync(roomImage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(roomImage, result);
        }
    }

}
