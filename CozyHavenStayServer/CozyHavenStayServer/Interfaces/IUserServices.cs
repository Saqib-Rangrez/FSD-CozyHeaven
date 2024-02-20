using CloudinaryDotNet.Actions;
using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IUserServices
    {
        //Users
        public Task<List<User>> GetAllUsersAsync(); 
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByEmailAsync(string name);
        public Task<User> CreateUserAsync(User user);
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(int id);
        public Task<ImageUploadResult> UploadDisplayPicture(int id, IFormFile file);

        //Reviws
        public Task<List<Review>> GetAllReviewsAsync();
        public Task<Review> GetReviwByReviewIdAsync(int id);
        public Task<List<Review>> GetReviewByHotelIdAsync(int id);
        public Task<List<Review>> GetReviewByUserIdAsync(int id);
        public Task<Review> AddReviewAsync(Review model);
        public Task<bool> UpdateReviewAsync(Review model);
        public Task<bool> DeleteReviewAsync(int id);

    }
}
