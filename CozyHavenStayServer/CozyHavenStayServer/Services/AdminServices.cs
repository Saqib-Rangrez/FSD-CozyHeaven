using CozyHavenStayServer.Controllers;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Repositories;

namespace CozyHavenStayServer.Services
{
    public class AdminServices : IAdminServices
    {
        
        private readonly IRepository<Admin> _adminRepository;
        private readonly ILogger<AdminController> _logger;

        public AdminServices(IRepository<Admin> adminRepository, ILogger<AdminController> logger)
        {
            _adminRepository = adminRepository;
            _logger = logger;
        }
        

        public async Task<Admin> CreateAdminAsync(Admin admin)
        {
            try
            {
                var createdAdmin = await _adminRepository.CreateAsync(admin);
                if(createdAdmin == null)
                {
                    _logger.LogError("Failed to add Admin");
                    return null;
                }
                return createdAdmin;
            }
            catch (Exception ex)
            {   
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            try
            {
                var admin = await _adminRepository.GetAsync(admin => admin.AdminId == id, false);

                if (admin == null)
                {
                    _logger.LogError("Admin not found with given Id");
                    return false;
                }

                await _adminRepository.DeleteAsync(admin);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {
            try
            {
                var admin = await _adminRepository.GetAsync(admin => admin.AdminId == id, false);

                if (admin == null)
                {
                    _logger.LogError("Admin not found with given Id");
                    return null;
                }
                return admin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
   

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            var emailLower = email.ToLower();
            var admin = await _adminRepository.GetAsync(user => user.Email.ToLower() == emailLower, false);

            if (admin == null)
            {
                _logger.LogError("Admin not found with given name");
                return null;
            }
            return admin;
        }

        public async Task<List<Admin>> GetAllAdminsAsync()
        {
            try
            {
                var admins = await _adminRepository.GetAllAsync();
                return admins;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateAdminAsync(Admin admin)
        {
            try
            {
                var adminUser = await _adminRepository.GetAsync(item => item.AdminId == admin.AdminId, true);

                if (adminUser == null)
                {
                    _logger.LogError("Admin not found with given Id");
                    return false;
                }

                await _adminRepository.UpdateAsync(admin);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
