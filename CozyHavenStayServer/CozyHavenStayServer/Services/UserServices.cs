using CloudinaryDotNet.Actions;
using CozyHavenStayServer.Controllers;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CozyHavenStayServer.Services
{
    public class UserServices : IUserServices
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Review> _reviewRepository;
        private readonly ILogger<UserController> _logger;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IConfiguration _configuration;
        private readonly IAuthServices _authServices;

        public UserServices(IRepository<User> userRepository, ILogger<UserController> logger, ICloudinaryService cloudinaryService, IConfiguration configuration, IRepository<Review> reviewRepository, IAuthServices authServices )
        {
            _authServices = authServices;
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _cloudinaryService = cloudinaryService;
            _configuration = configuration;
            _logger = logger;
        }


        #region User Actions
        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetAsync(user => user.UserId == id, false);

                if (user == null)
                {
                    _logger.LogError("User not found with given Id");
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var emailLower = email.ToLower();
            var user = await _userRepository.GetAsync(user => user.Email.ToLower() == emailLower, false);

            if (user == null)
            {
                _logger.LogError("User not found with given name");
                return null;
            }
            return user;
        }

        public async Task<User> CreateUserAsync(User model)
        {
            try
            {
                var createdUser = await _userRepository.CreateAsync(model);
                return createdUser;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }            
        }               

        public async Task<bool> UpdateUserAsync(User model)
        {
            try
            {
                var user = await _userRepository.GetAsync(item => item.UserId == model.UserId, true);

                if (user == null)
                {
                    _logger.LogError("User not found with given Id");
                    return false;
                }

                var hashedPassword = _authServices.HashPassword(model.Password);
                model.Password = hashedPassword;


                await _userRepository.UpdateAsync(model);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }            
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetAsync(user => user.UserId == id, false);

                if (user == null)
                {
                    _logger.LogError("User not found with given Id");
                    return false;
                }

                await _userRepository.DeleteAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        #endregion

        #region Review Actions

        public async Task<Review> AddReviewAsync(Review model)
        {
            try
            {
                var createdReview = await _reviewRepository.CreateAsync(model);
                if(createdReview == null)
                {
                    return null;
                }
                return createdReview;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            try
            {
                var reviews = await _reviewRepository.GetAllAsync();
                return reviews;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Review> GetReviewByReviewIdAsync(int id)
        {
            try
            {
                var review = await _reviewRepository.GetAsync(review => review.ReviewId == id, false);

                if (review == null)
                {
                    _logger.LogError("Reviw not found with given Id");
                    return null;
                }
                return review;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Review>> GetReviewByUserIdAsync(int id)
        {
            try
            {
                var reviews = await _reviewRepository.GetAllAsync();
                reviews = reviews.Where(review => review.UserId == id).ToList();

                if (reviews == null)
                {
                    _logger.LogError("Reviw not found with given Id");
                    return null;
                }
                return reviews;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Review>> GetReviewByHotelIdAsync(int id)
        {
            try
            {
                var reviews = await _reviewRepository.GetAllAsync();
                reviews = reviews.Where(review => review.HotelId == id).ToList();
                if (reviews == null)
                {
                    _logger.LogError("Reviw not found with given Id");
                    return null;
                }
                return reviews;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateReviewAsync(Review model)
        {
            try
            {
                var review = await _reviewRepository.GetAsync(item => item.ReviewId == model.ReviewId, true);

                if (review == null)
                {
                    _logger.LogError("Review not found with given Id");
                    return false;
                }

                await _reviewRepository.UpdateAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            try
            {
                var review = await _reviewRepository.GetAsync(item => item.ReviewId == id, false);

                if (review == null)
                {
                    _logger.LogError("User not found with given Id");
                    return false;
                }

                await _reviewRepository.DeleteAsync(review);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        #endregion

    }
}
