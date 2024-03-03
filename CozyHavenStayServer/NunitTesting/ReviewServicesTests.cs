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
    public class ReviewServicesTests
    {
        private Mock<IRepository<Review>> _reviewRepositoryMock;
        private Mock<IRepository<User>> _userRepositoryMock;
        
        private Mock<ILogger<UserController>> _loggerMock;
        private IUserServices _reviewServices;

        [SetUp]
        public void Setup()
        {
            _reviewRepositoryMock = new Mock<IRepository<Review>>();
            _userRepositoryMock = new Mock<IRepository<User>>();
           
            _loggerMock = new Mock<ILogger<UserController>>();
            _reviewServices = new UserServices(_userRepositoryMock.Object, _loggerMock.Object, null, null, _reviewRepositoryMock.Object);
        }

        [Test]
        public async Task AddReviewAsync_ReturnsCreatedReview_WhenSuccessful()
        {
            // Arrange
            var review = new Review { ReviewId = 1, UserId = 1, HotelId = 1, Rating = 5, Comments = "Excellent service!" };
            _reviewRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Review>())).ReturnsAsync(review);

            // Act
            var result = await _reviewServices.AddReviewAsync(review);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(review.ReviewId, result.ReviewId);
            Assert.AreEqual(review.UserId, result.UserId);
            Assert.AreEqual(review.HotelId, result.HotelId);
            Assert.AreEqual(review.Rating, result.Rating);
            Assert.AreEqual(review.Comments, result.Comments);
        }

        [Test]
        public async Task GetAllReviewsAsync_ReturnsListOfReviews_WhenSuccessful()
        {
            // Arrange
            var reviews = new List<Review>
            {
                new Review { ReviewId = 1, UserId = 1, HotelId = 1, Rating = 5, Comments = "Excellent service!" },
                new Review { ReviewId = 2, UserId = 2, HotelId = 2, Rating = 4, Comments = "Great experience!" }
            };
            _reviewRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(reviews);

            // Act
            var result = await _reviewServices.GetAllReviewsAsync();

            // Assert
            Assert.IsNotNull(result);
           
            Assert.AreEqual(reviews[0].ReviewId, result[0].ReviewId);
            Assert.AreEqual(reviews[1].Comments, result[1].Comments);
        }

        [Test]
        public async Task GetReviewByReviewIdAsync_ReturnsReview_WhenFound()
        {
            // Arrange
            int reviewId = 1;
            var review = new Review { ReviewId = reviewId, UserId = 1, HotelId = 1, Rating = 5, Comments = "Excellent service!" };
            _reviewRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Review, bool>>>(), false)).ReturnsAsync(review);

            // Act
            var result = await _reviewServices.GetReviewByReviewIdAsync(reviewId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(review.ReviewId, result.ReviewId);
            Assert.AreEqual(review.Rating, result.Rating);
            Assert.AreEqual(review.Comments, result.Comments);
        }

        [Test]
        public async Task GetReviewByUserIdAsync_ReturnsListOfReviews_WhenFound()
        {
            // Arrange
            int userId = 1;
            var reviews = new List<Review>
            {
                new Review { ReviewId = 1, UserId = userId, HotelId = 1, Rating = 5, Comments = "Excellent service!" },
                new Review { ReviewId = 2, UserId = userId, HotelId = 2, Rating = 4, Comments = "Great experience!" }
            };
            _reviewRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(reviews);

            // Act
            var result = await _reviewServices.GetReviewByUserIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            
            Assert.AreEqual(reviews[0].ReviewId, result[0].ReviewId);
            Assert.AreEqual(reviews[1].Comments, result[1].Comments);
        }

        [Test]
        public async Task GetReviewByHotelIdAsync_ReturnsListOfReviews_WhenFound()
        {
            // Arrange
            int hotelId = 1;
            var reviews = new List<Review>
            {
                new Review { ReviewId = 1, UserId = 1, HotelId = hotelId, Rating = 5, Comments = "Excellent service!" },
                new Review { ReviewId = 2, UserId = 2, HotelId = hotelId, Rating = 4, Comments = "Great experience!" }
            };
            _reviewRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(reviews);

            // Act
            var result = await _reviewServices.GetReviewByHotelIdAsync(hotelId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(reviews[0].ReviewId, result[0].ReviewId);
            Assert.AreEqual(reviews[1].Comments, result[1].Comments);
        }

        [Test]
        public async Task UpdateReviewAsync_ReturnsTrue_WhenReviewUpdatedSuccessfully()
        {
            // Arrange
            int reviewId = 1;
            var review = new Review { ReviewId = reviewId, UserId = 1, HotelId = 1, Rating = 5, Comments = "Excellent service!" };
            _reviewRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Review, bool>>>(), true)).ReturnsAsync(review);

            // Act
            var result = await _reviewServices.UpdateReviewAsync(review);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteReviewAsync_ReturnsTrue_WhenReviewDeletedSuccessfully()
        {
            // Arrange
            int reviewId = 1;
            var review = new Review { ReviewId = reviewId, UserId = 1, HotelId = 1, Rating = 5, Comments = "Excellent service!" };
            _reviewRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Review, bool>>>(), false)).ReturnsAsync(review);

            // Act
            var result = await _reviewServices.DeleteReviewAsync(reviewId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
