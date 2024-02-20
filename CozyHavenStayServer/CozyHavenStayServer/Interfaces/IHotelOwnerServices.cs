using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IHotelOwnerServices
    {
        public Task<List<HotelOwner>> GetAllHotelOwnersAsync();
        public Task<HotelOwner> GetHotelOwnerByIdAsync(int id);
        public Task<HotelOwner> GetHotelOwnerByEmailAsync(string email);
        public Task<HotelOwner> CreateHotelOwnerAsync(HotelOwner hotelOwner);
        public Task<bool> UpdateHotelOwnerAsync(HotelOwner hotelOwner);
        public Task<bool> DeleteHotelOwnerAsync(int id);

        //Booking
        public Task<List<Booking>> GetAllBookingsAsync();
        public Task<Booking> GetBookingByIdAsync(int id);
        public Task<Booking> CreateBookingAsync(Booking booking);
        public Task<bool> UpdateBookingAsync(Booking booking);
        public Task<bool> DeleteBookingAsync(int id);

        //Room
        public Task<List<Room>> GetAllRoomsAsync();
        public Task<Room> GetRoomByIdAsync(int id);
        public Task<Room> CreateRoomAsync(Room room);
        public Task<bool> UpdateRoomAsync(Room room);
        public Task<bool> DeleteRoomAsync(int id);

        //hotel
        public Task<List<Hotel>> GetAllHotelsAsync();
        public Task<Hotel> GetHotelByIdAsync(int id);
        public Task<Hotel> GetHotelByNameAsync(string name);
        public Task<Hotel> CreateHotelAsync(Hotel hotel);
        public Task<bool> UpdateHotelAsync(Hotel hotel);
        public Task<bool> DeleteHotelAsync(int id);

    }
}
