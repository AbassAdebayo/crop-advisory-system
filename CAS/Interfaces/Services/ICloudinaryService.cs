using CAS.DTOs;

namespace CAS.Interfaces.Services
{
    public interface ICloudinaryService
    {
        Task<BaseResponse<(string Url, string PublicId)>> UploadImageAsync(IFormFile file);
        Task DeleteImageAsync(string publicId);
    }
}
