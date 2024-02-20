using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IUserServices
    {
        public Task<List<User>> GetAllUsersAsync(); 
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByNameAsync(string name);
        public Task<User> CreateUserAsync(User user);
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(int id);


        //booking
        public Task<Booking> GetBookingByIdAsync(int id);
        public Task<Booking> CreateBookingAsync(Booking booking);
        public Task<bool> UpdateBookingAsync(Booking booking);
        public Task<bool> DeleteBookingAsync(int id);



        //hotel
        public Task<List<Hotel>> GetAllHotelsAsync();
        public Task<Hotel> GetHotelByIdAsync(int id);
        public Task<Hotel> GetHotelByNameAsync(string name);
       


        //Room
         public Task<List<Room>> GetAllRoomsAsync();
        public Task<Room> GetRoomByIdAsync(int id);
        public Task<Room> CreateRoomAsync(Room room);
        public Task<bool> UpdateRoomAsync(Room room);
        public Task<bool> DeleteRoomAsync(int id);
    }
}
