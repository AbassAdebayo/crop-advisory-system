using CAS.DTOs;
using CAS.DTOs.Crop;

namespace CAS.Interfaces.Services
{
    public interface ICropService
    {
        public Task<BaseResponse> CreateCropAsync(CreateCropRequestModel model);
        public Task<BaseResponse<IReadOnlyList<CropListResponseModel>>> GetAllCropsAsync();
        public Task<BaseResponse<CropDto>> GetCropByIdAsync(Guid cropId);
    }
}
