using CozyHavenStayServer.Controllers;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NunitTesting
{
    [TestFixture]
    public class UserServicesTests
    {
        private Mock<IRepository<User>> _userRepositoryMock;
        private Mock<IRepository<Review>> _reviewRepositoryMock;
        private Mock<ILogger<UserController>> _loggerMock;
        private Mock<IAuthServices> _authSevicesMock;
        private UserServices _userServices;
        private Mock<ICloudinaryService> _cloudinaryService;
        private Mock<IConfiguration> _configuration;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
            _reviewRepositoryMock = new Mock<IRepository<Review>>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _authSevicesMock = new Mock<IAuthServices>();
            _cloudinaryService = new Mock<ICloudinaryService>();
            _configuration = new Mock<IConfiguration>();
            _userServices = new UserServices(_userRepositoryMock.Object, _loggerMock.Object, _cloudinaryService.Object, _configuration.Object, _reviewRepositoryMock.Object, _authSevicesMock.Object);
        }

        [Test]
        public async Task GetAllUsersAsync_ReturnsListOfUsers_WhenSuccessful()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new User { UserId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
            };
            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userServices.GetAllUsersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(users.Count, result.Count);
            Assert.AreEqual(users[0].UserId, result[0].UserId);
            Assert.AreEqual(users[1].Email, result[1].Email);
        }

        [Test]
        public async Task GetUserByIdAsync_ReturnsUser_WhenFound()
        {
            // Arrange
            int userId = 1;
            var user = new User { UserId = userId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), false)).ReturnsAsync(user);

            // Act
            var result = await _userServices.GetUserByIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserId, result.UserId);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Email, result.Email);
        }

        [Test]
        public async Task GetUserByEmailAsync_ReturnsUser_WhenFound()
        {
            // Arrange
            string email = "john.doe@example.com";
            var user = new User { UserId = 1, FirstName = "John", LastName = "Doe", Email = email };
            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), false)).ReturnsAsync(user);

            // Act
            var result = await _userServices.GetUserByEmailAsync(email);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserId, result.UserId);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Email, result.Email);
        }

        [Test]
        public async Task CreateUserAsync_ReturnsCreatedUser_WhenSuccessful()
        {
            // Arrange
            var user = new User { UserId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userServices.CreateUserAsync(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserId, result.UserId);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Email, result.Email);
        }

        [Test]
        public async Task UpdateUserAsync_ReturnsUpdatedUser_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            var user = new User { UserId = userId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            // Set up the mock repository to return the user when UserId matches
            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), true))
                               .ReturnsAsync(user);

            // Act
            var result = await _userServices.UpdateUserAsync(user);

            // Assert
            Assert.IsTrue(result);
        }


        [Test]
        public async Task DeleteUserAsync_ReturnsTrue_WhenUserDeletedSuccessfully()
        {
            // Arrange
            int userId = 1;
            var user = new User { UserId = userId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), false)).ReturnsAsync(user);

            // Act
            var result = await _userServices.DeleteUserAsync(userId);

            // Assert
            Assert.IsTrue(result);
        }
    }

}
