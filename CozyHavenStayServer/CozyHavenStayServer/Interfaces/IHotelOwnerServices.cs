using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IHotelOwnerServices
    {
        public Task<List<HotelOwner>> GetAllHotelOwnersAsync();
        public Task<HotelOwner> GetHotelOwnerByIdAsync(int id);
        public Task<HotelOwner> GetHotelOwnerByEmailAsync(string email);
        public Task<HotelOwner> CreateHotelOwnerAsync(HotelOwner hotelOwner);
        public Task<bool> UpdateHotelOwnerAsync(HotelOwner hotelOwner, bool flag = true);
        public Task<bool> DeleteHotelOwnerAsync(int id);

    }
}
