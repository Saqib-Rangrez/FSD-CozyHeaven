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
    public class RefundServiceTests
    {
        private Mock<IRepository<Refund>> _refundRepositoryMock;
        private Mock<ILogger<RefundService>> _loggerMock;
        private RefundService _refundService;

        [SetUp]
        public void Setup()
        {
            _refundRepositoryMock = new Mock<IRepository<Refund>>();
            _loggerMock = new Mock<ILogger<RefundService>>();
            _refundService = new RefundService(_loggerMock.Object, _refundRepositoryMock.Object);
        }

        [Test]
        public async Task CreateRefundAsync_ReturnsCreatedRefund_WhenSuccessful()
        {
            // Arrange
            var refund = new Refund { RefundId = 1, PaymentId = 1, RefundAmount = 100, RefundDate = DateTime.Now };
            _refundRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Refund>())).ReturnsAsync(refund);

            // Act
            var result = await _refundService.CreateRefundAsync(refund);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(refund, result);
        }

        [Test]
        public async Task DeleteRefundAsync_ReturnsTrue_WhenRefundDeletedSuccessfully()
        {
            // Arrange
            int refundId = 1;
            var refund = new Refund { RefundId = refundId, PaymentId = 1, RefundAmount = 100, RefundDate = DateTime.Now };
            _refundRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Refund, bool>>>(),false)).ReturnsAsync(refund);

            // Act
            var result = await _refundService.DeleteRefundAsync(refundId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAllRefundsAsync_ReturnsListOfRefunds_WhenSuccessful()
        {
            // Arrange
            var refunds = new List<Refund>
            {
                new Refund { RefundId = 1, PaymentId = 1, RefundAmount = 100, RefundDate = DateTime.Now },
                new Refund { RefundId = 2, PaymentId = 2, RefundAmount = 200, RefundDate = DateTime.Now }
            };
            _refundRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(refunds);

            // Act
            var result = await _refundService.GetAllRefundsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(refunds.Count, result.Count);
            Assert.AreEqual(refunds[0], result[0]);
            Assert.AreEqual(refunds[1], result[1]);
        }

        [Test]
        public async Task GetRefundByIdAsync_ReturnsRefund_WhenFound()
        {
            // Arrange
            int refundId = 1;
            var refund = new Refund { RefundId = refundId, PaymentId = 1, RefundAmount = 100, RefundDate = DateTime.Now };
            _refundRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Refund, bool>>>(),false)).ReturnsAsync(refund);

            // Act
            var result = await _refundService.GetRefundByIdAsync(refundId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(refund, result);
        }

        [Test]
        public async Task GetRefundByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            int refundId = 1;
            _refundRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Refund, bool>>>(),false)).ReturnsAsync((Refund)null);

            // Act
            var result = await _refundService.GetRefundByIdAsync(refundId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetRefundByPaymentIdAsync_ReturnsRefund_WhenFound()
        {
            // Arrange
            int paymentId = 1;
            var refund = new Refund { RefundId = 1, PaymentId = paymentId, RefundAmount = 100, RefundDate = DateTime.Now };
            _refundRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Refund, bool>>>(), false)).ReturnsAsync(refund);

            // Act
            var result = await _refundService.GetRefundByPaymentIdAsync(paymentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(refund, result);
        }

        [Test]
        public async Task GetRefundByPaymentIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            int paymentId = 1;
            _refundRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Refund, bool>>>(),false)).ReturnsAsync((Refund)null);

            // Act
            var result = await _refundService.GetRefundByPaymentIdAsync(paymentId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateRefundAsync_ReturnsTrue_WhenRefundUpdatedSuccessfully()
        {
            // Arrange
            var refund = new Refund { RefundId = 1, PaymentId = 1, RefundAmount = 100, RefundDate = DateTime.Now };
            _refundRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Refund, bool>>>(),false)).ReturnsAsync(refund);

            // Act
            var result = await _refundService.UpdateRefundAsync(refund);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateRefundAsync_ReturnsFalse_WhenRefundNotFound()
        {
            // Arrange
            var refund = new Refund { RefundId = 1, PaymentId = 1, RefundAmount = 100, RefundDate = DateTime.Now };
            _refundRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Refund, bool>>>(),false)).ReturnsAsync((Refund)null);
            
            // Act
            var result = await _refundService.UpdateRefundAsync(refund);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ApproveRefundAsync_ReturnsTrue_WhenRefundApprovedSuccessfully()
        {
            // Arrange
            int refundId = 1;
            var refund = new Refund { RefundId = refundId, PaymentId = 1, RefundAmount = 100, RefundDate = DateTime.Now };
            _refundRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Refund, bool>>>(), true)).ReturnsAsync(refund);

            // Act
            var result = await _refundService.ApproveRefundAsync(refundId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ApproveRefundAsync_ReturnsFalse_WhenRefundNotFound()
        {
            // Arrange
            int refundId = 1;
            _refundRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Refund, bool>>>(), true)).ReturnsAsync((Refund)null);

            // Act
            var result = await _refundService.ApproveRefundAsync(refundId);

            // Assert
            Assert.IsFalse(result);
        }
    }

}
