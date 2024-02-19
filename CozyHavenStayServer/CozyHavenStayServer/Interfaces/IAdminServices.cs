using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IAdminServices
    {
        public Task<List<Admin>> GetAllAdminsAsync();
        public Task<Admin> GetAdminByIdAsync(int id);
        public Task<Admin> GetAdminByEmailAsync(string email);
        public Task<Admin> CreateAdminAsync(Admin admin);
        public Task<bool> UpdateAdminAsync(Admin admin);
        public Task<bool> DeleteAdminAsync(int id);
    }
}
