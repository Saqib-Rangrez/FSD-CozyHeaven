using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IUserServices
    {
        public Task<List<User>> GetAllUsersAsync(); 
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByEmailAsync(string name);
        public Task<User> CreateUserAsync(User user);
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(int id);

    }
}
