using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IHotelOwnerServices
    {
        public Task<List<HotelOwner>> GetAllHotelOwnersAsync();
        public Task<HotelOwner> GetHotelOwnerByIdAsync(int id);
        public Task<HotelOwner> GetHotelOwnerByNameAsync(string name);
        public Task<HotelOwner> CreateHotelOwnerAsync(HotelOwner hotelOwner);
        public Task<bool> UpdateHotelOwnerAsync(HotelOwner hotelOwner);
        public Task<bool> DeleteHotelOwnerAsync(int id);

    }
}
