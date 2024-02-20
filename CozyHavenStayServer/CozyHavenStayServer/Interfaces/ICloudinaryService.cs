using CloudinaryDotNet.Actions;

namespace CozyHavenStayServer.Interfaces
{
    public interface ICloudinaryService
    {
        public Task<ImageUploadResult> UploadImageAsync(IFormFile file, string folder, int? height = null, int? quality = null);
    }
}
