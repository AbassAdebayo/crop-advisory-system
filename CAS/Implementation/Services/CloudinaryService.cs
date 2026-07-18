using CAS.Configuration;
using CAS.DTOs;
using CAS.Interfaces.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace CAS.Implementation.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinarySettings> options)
        {
            var account = new Account(
                options.Value.CloudName,
                options.Value.ApiKey,
                options.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);

            // _cloudinary = cloudinary ?? throw new ArgumentNullException(nameof(cloudinary));
        }

        public async Task DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId)) return;
            await _cloudinary.DestroyAsync(new DeletionParams(publicId));
        }

        public async Task<BaseResponse<(string Url, string PublicId)>> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new BaseResponse<(string Url, string PublicId)> { Message = "Image is required.", IsSuccess = false };

            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "crop-advisory/crops"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
                return new BaseResponse<(string Url, string PublicId)> { Message = $"{result.Error.Message}", IsSuccess = false };

            return new BaseResponse<(string Url, string PublicId)>
            {
                Message = " Image uploaded successfully",
                IsSuccess = true,
                Data = (result.SecureUrl.ToString(), result.PublicId)
            };
        }
    }
}
