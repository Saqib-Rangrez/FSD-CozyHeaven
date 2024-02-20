using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IHotelServices
    {
        public Task<List<Hotel>> GetAllHotelsAsync();
        public Task<Hotel> GetHotelByIdAsync(int id);
        public Task<Hotel> GetHotelByNameAsync(string name);
        public Task<Hotel> CreateHotelAsync(Hotel hotel);
        public Task<bool> UpdateHotelAsync(Hotel hotel);
        public Task<bool> DeleteHotelAsync(int id);
    }
}
