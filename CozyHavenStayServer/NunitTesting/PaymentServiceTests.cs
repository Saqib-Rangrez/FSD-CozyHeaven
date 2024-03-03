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
    public class PaymentServiceTests
    {
        private Mock<IRepository<Payment>> _paymentRepositoryMock;
        private Mock<ILogger<PaymentService>> _loggerMock;
        private PaymentService _paymentService;

        [SetUp]
        public void Setup()
        {
            _paymentRepositoryMock = new Mock<IRepository<Payment>>();
            _loggerMock = new Mock<ILogger<PaymentService>>();
            _paymentService = new PaymentService(_loggerMock.Object, _paymentRepositoryMock.Object);
        }

        [Test]
        public async Task CreatePaymentAsync_ReturnsCreatedPayment_WhenSuccessful()
        {
            // Arrange
            var payment = new Payment { PaymentId = 1, BookingId = 1, Amount = 100, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Payment>())).ReturnsAsync(payment);

            // Act
            var result = await _paymentService.CreatePaymentAsync(payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(payment, result);
        }

        [Test]
        public async Task DeletePaymentAsync_ReturnsTrue_WhenPaymentDeletedSuccessfully()
        {
            // Arrange
            int paymentId = 1;
            var payment = new Payment { PaymentId = paymentId, BookingId = 1, Amount = 100, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Payment, bool>>>(), false)).ReturnsAsync(payment);

            // Act
            var result = await _paymentService.DeletePaymentAsync(paymentId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAllPaymentsAsync_ReturnsListOfPayments_WhenSuccessful()
        {
            // Arrange
            var payments = new List<Payment>
            {
                new Payment { PaymentId = 1, BookingId = 1, Amount = 100, PaymentDate = DateTime.Now },
                new Payment { PaymentId = 2, BookingId = 2, Amount = 200, PaymentDate = DateTime.Now }
            };
            _paymentRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(payments);

            // Act
            var result = await _paymentService.GetAllPaymentsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(payments.Count, result.Count);
            Assert.AreEqual(payments[0], result[0]);
            Assert.AreEqual(payments[1], result[1]);
        }

        [Test]
        public async Task GetPaymentByIdAsync_ReturnsPayment_WhenFound()
        {
            // Arrange
            int paymentId = 1;
            var payment = new Payment { PaymentId = paymentId, BookingId = 1, Amount = 100, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Payment, bool>>>(), false)).ReturnsAsync(payment);

            // Act
            var result = await _paymentService.GetPaymentByIdAsync(paymentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(payment, result);
        }

        [Test]
        public async Task GetPaymentByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            int paymentId = 1;
            _paymentRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Payment, bool>>>(),false)).ReturnsAsync((Payment)null);

            // Act
            var result = await _paymentService.GetPaymentByIdAsync(paymentId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetPaymentByBookingIdAsync_ReturnsPayment_WhenFound()
        {
            // Arrange
            int bookingId = 1;
            var payment = new Payment { PaymentId = 1, BookingId = bookingId, Amount = 100, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Payment, bool>>>(), false)).ReturnsAsync(payment);

            // Act
            var result = await _paymentService.GetPaymentByBookingIdAsync(bookingId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(payment, result);
        }

        [Test]
        public async Task GetPaymentByBookingIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            int bookingId = 1;
            _paymentRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Payment, bool>>>(), false)).ReturnsAsync((Payment)null);

            // Act
            var result = await _paymentService.GetPaymentByBookingIdAsync(bookingId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdatePaymentAsync_ReturnsTrue_WhenPaymentUpdatedSuccessfully()
        {
            // Arrange
            var payment = new Payment { PaymentId = 1, BookingId = 1, Amount = 100, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Payment, bool>>>(), false)).ReturnsAsync(payment);

            // Act
            var result = await _paymentService.UpdatePaymentAsync(payment);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdatePaymentAsync_ReturnsFalse_WhenPaymentNotFound()
        {
            // Arrange
            var payment = new Payment { PaymentId = 1, BookingId = 1, Amount = 100, PaymentDate = DateTime.Now };
            _paymentRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Payment, bool>>>(),false)).ReturnsAsync((Payment)null);

            // Act
            var result = await _paymentService.UpdatePaymentAsync(payment);

            // Assert
            Assert.IsFalse(result);
        }
    }
    }
