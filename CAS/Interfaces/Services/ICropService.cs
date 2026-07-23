using CAS.Contracts.Enums;
using CAS.DTOs;
using CAS.DTOs.Crop;

namespace CAS.Interfaces.Services
{
    public interface ICropService
    {
        public Task<BaseResponse> CreateCropAsync(string imageUrl, CreateCropRequestModel model);
        public Task<BaseResponse<IReadOnlyList<CropListResponseModel>>> GetAllCropsAsync();
        public Task<BaseResponse<IReadOnlyList<CropListResponseModel>>> GetAllCropsForAdminAsync();
        public Task<BaseResponse<CropDto>> GetCropByIdAsync(Guid cropId);
        public Task<BaseResponse> ActivateCropStatusAsync(Guid id);
        public Task<BaseResponse> DeactivateCropStatusAsync(Guid id);
    }
}
