using CozyHavenStayServer.Controllers;
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
    public class HotelOwnerServicesTests
    {
        private Mock<IRepository<HotelOwner>> _hotelOwnerRepositoryMock;
        private Mock<ILogger<HotelOwnerController>> _loggerMock;
        private IHotelOwnerServices _hotelOwnerServices;
        private Mock<IAuthServices> _authSevicesMock;


        [SetUp]
        public void Setup()
        {
            _hotelOwnerRepositoryMock = new Mock<IRepository<HotelOwner>>();
            _loggerMock = new Mock<ILogger<HotelOwnerController>>();
            _authSevicesMock = new Mock<IAuthServices>();
            _hotelOwnerServices = new HotelOwnerServices(_hotelOwnerRepositoryMock.Object, _loggerMock.Object,_authSevicesMock.Object);
        }

        [Test]
        public async Task CreateHotelOwnerAsync_ReturnsCreatedHotelOwner_WhenSuccessful()
        {
            // Arrange
            var hotelOwner = new HotelOwner { OwnerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _hotelOwnerRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<HotelOwner>())).ReturnsAsync(hotelOwner);

            // Act
            var result = await _hotelOwnerServices.CreateHotelOwnerAsync(hotelOwner);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hotelOwner.OwnerId, result.OwnerId);
            Assert.AreEqual(hotelOwner.FirstName, result.FirstName);
            Assert.AreEqual(hotelOwner.LastName, result.LastName);
            Assert.AreEqual(hotelOwner.Email, result.Email);
        }

        [Test]
        public async Task DeleteHotelOwnerAsync_ReturnsTrue_WhenHotelOwnerDeletedSuccessfully()
        {
            // Arrange
            int ownerId = 1;
            var hotelOwner = new HotelOwner { OwnerId = ownerId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _hotelOwnerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<HotelOwner, bool>>>(), false)).ReturnsAsync(hotelOwner);

            // Act
            var result = await _hotelOwnerServices.DeleteHotelOwnerAsync(ownerId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAllHotelOwnersAsync_ReturnsListOfHotelOwners_WhenSuccessful()
        {
            // Arrange
            var hotelOwners = new List<HotelOwner>
            {
                new HotelOwner { OwnerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new HotelOwner { OwnerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
            };
            _hotelOwnerRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(hotelOwners);

            // Act
            var result = await _hotelOwnerServices.GetAllHotelOwnersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hotelOwners.Count, result.Count);
            Assert.AreEqual(hotelOwners[0].OwnerId, result[0].OwnerId);
            Assert.AreEqual(hotelOwners[1].Email, result[1].Email);
        }

        [Test]
        public async Task GetHotelOwnerByIdAsync_ReturnsHotelOwner_WhenFound()
        {
            // Arrange
            int ownerId = 1;
            var hotelOwner = new HotelOwner { OwnerId = ownerId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _hotelOwnerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<HotelOwner, bool>>>(), false)).ReturnsAsync(hotelOwner);

            // Act
            var result = await _hotelOwnerServices.GetHotelOwnerByIdAsync(ownerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hotelOwner.OwnerId, result.OwnerId);
            Assert.AreEqual(hotelOwner.FirstName, result.FirstName);
            Assert.AreEqual(hotelOwner.LastName, result.LastName);
            Assert.AreEqual(hotelOwner.Email, result.Email);
        }

        [Test]
        public async Task GetHotelOwnerByEmailAsync_ReturnsHotelOwner_WhenFound()
        {
            // Arrange
            string email = "john.doe@example.com";
            var hotelOwner = new HotelOwner { OwnerId = 1, FirstName = "John", LastName = "Doe", Email = email };
            _hotelOwnerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<HotelOwner, bool>>>(), false)).ReturnsAsync(hotelOwner);

            // Act
            var result = await _hotelOwnerServices.GetHotelOwnerByEmailAsync(email);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hotelOwner.OwnerId, result.OwnerId);
            Assert.AreEqual(hotelOwner.FirstName, result.FirstName);
            Assert.AreEqual(hotelOwner.LastName, result.LastName);
            Assert.AreEqual(hotelOwner.Email, result.Email);
        }

        [Test]
        public async Task UpdateHotelOwnerAsync_ReturnsTrue_WhenHotelOwnerUpdatedSuccessfully()
        {
            // Arrange
            int ownerId = 1;
            var hotelOwner = new HotelOwner { OwnerId = ownerId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _hotelOwnerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<HotelOwner, bool>>>(), true)).ReturnsAsync(hotelOwner);

            // Act
            var result = await _hotelOwnerServices.UpdateHotelOwnerAsync(hotelOwner);

            // Assert
            Assert.IsTrue(result);
        }
    }
  }
