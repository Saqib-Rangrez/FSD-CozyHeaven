using CloudinaryDotNet.Actions;

namespace CozyHavenStayServer.Interfaces
{
    public interface ICloudinaryService
    {
        public Task<ImageUploadResult> UploadImageAsync(IFormFile file, int? height = null, int? quality = null);

    }
}
