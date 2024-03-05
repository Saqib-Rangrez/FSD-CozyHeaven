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
    public class BookingServicesTests
    {
        private Mock<ILogger<BookingServices>> _loggerMock;
        private Mock<IRepository<Booking>> _bookingRepositoryMock;
        private BookingServices _bookingServices;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<BookingServices>>();
            _bookingRepositoryMock = new Mock<IRepository<Booking>>();
            _bookingServices = new BookingServices(_loggerMock.Object, _bookingRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllBookingsAsync_ReturnsListOfBookings_WhenBookingsExist()
        {
            // Arrange
            var bookings = new List<Booking> { new Booking { BookingId = 1, UserId = 1, RoomId = 1, HotelId = 1, PaymentId = 1, NumberOfGuests = 2, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(2), TotalFare = 200.0m, Status = "Confirmed" } };
            _bookingRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bookings);

            // Act
            var result = await _bookingServices.GetAllBookingsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(bookings.Count));
            
        }

        [Test]
        public async Task GetBookingByIdAsync_ReturnsBooking_WhenBookingExists()
        {
            // Arrange
            int bookingId = 1;
            var booking = new Booking { BookingId = bookingId, UserId = 1, RoomId = 1, HotelId = 1, PaymentId = 1, NumberOfGuests = 2, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(2), TotalFare = 200.0m, Status = "Confirmed" };
            _bookingRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Booking, bool>>>(), false)).ReturnsAsync(booking);

            // Act
            var result = await _bookingServices.GetBookingByIdAsync(bookingId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bookingId, result.BookingId);
           
        }

        [Test]
        public async Task GetBookingByIdAsync_ReturnsNull_WhenBookingDoesNotExist()
        {
            // Arrange
            var nonExistentBookingId = 999;
            _bookingRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Booking, bool>>>(), false)).ReturnsAsync((Booking)null);

            // Act
            var result = await _bookingServices.GetBookingByIdAsync(nonExistentBookingId);

            // Assert
            Assert.IsNull(result);
        }


        [Test]
        public async Task CreateBookingAsync_ReturnsCreatedBooking_WhenValidBookingProvided()
        {
            // Arrange
            var validBooking = new Booking { UserId = 1, RoomId = 1, HotelId = 1, PaymentId = 1, NumberOfGuests = 2, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(2), TotalFare = 200.0m, Status = "Confirmed" };
            _bookingRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Booking>())).ReturnsAsync(validBooking);

            // Act
            var result = await _bookingServices.CreateBookingAsync(validBooking);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(validBooking, result);
        }

        [Test]
        public async Task CreateBookingAsync_ReturnsNull_WhenInvalidBookingProvided()
        {
            // Arrange
            var invalidBooking = new Booking { }; // Provide invalid booking data here
            _bookingRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Booking>())).ReturnsAsync((Booking)null);

            // Act
            var result = await _bookingServices.CreateBookingAsync(invalidBooking);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateBookingAsync_ReturnsTrue_WhenBookingUpdatedSuccessfully()
        {
            // Arrange
            var existingBooking = new Booking { BookingId = 1, UserId = 1, RoomId = 1, HotelId = 1, PaymentId = 1, NumberOfGuests = 2, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(2), TotalFare = 200.0m, Status = "Confirmed" };
            _bookingRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Booking, bool>>>(), true)).ReturnsAsync(existingBooking);

            // Act
            var result = await _bookingServices.UpdateBookingAsync(existingBooking);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateBookingAsync_ReturnsFalse_WhenBookingToUpdateDoesNotExist()
        {
            // Arrange
            var nonExistentBooking = new Booking { BookingId = 999 }; // Provide a non-existent booking ID here
            _bookingRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Booking, bool>>>(), true)).ReturnsAsync((Booking)null);

            // Act
            var result = await _bookingServices.UpdateBookingAsync(nonExistentBooking);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteBookingAsync_ReturnsTrue_WhenBookingDeletedSuccessfully()
        {
            // Arrange
            var existingBookingId = 1; // Provide an existing booking ID here
            var existingBooking = new Booking { BookingId = existingBookingId };
            _bookingRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Booking, bool>>>(), false)).ReturnsAsync(existingBooking);

            // Act
            var result = await _bookingServices.DeleteBookingAsync(existingBookingId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteBookingAsync_ReturnsFalse_WhenBookingToDeleteDoesNotExist()
        {
            // Arrange
            var nonExistentBookingId = 999; // Provide a non-existent booking ID here
            _bookingRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Booking, bool>>>(), false)).ReturnsAsync((Booking)null);

            // Act
            var result = await _bookingServices.DeleteBookingAsync(nonExistentBookingId);

            // Assert
            Assert.IsFalse(result);
        }

    }
}
