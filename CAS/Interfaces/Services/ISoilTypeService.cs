using CAS.DTOs;
using CAS.DTOs.SoilType;

namespace CAS.Interfaces.Services
{
    public interface ISoilTypeService
    {
        Task<BaseResponse<IReadOnlyList<SoilTypeListResponseModel>>> GetAllSoilTypesForAdminAsync();
        Task<BaseResponse<IReadOnlyList<SoilTypeListResponseModel>>> GetAllSoilTypesForFarmerAsync();

        Task<BaseResponse<SoilTypeDto>> GetSoilTypeDetailsForAdminAsync(Guid id);
        Task<BaseResponse<SoilTypeDto>> GetSoilTypeDetailsForFarmerAsync(Guid id);

        public Task<BaseResponse> ActivateSoilTypeStatusAsync(Guid id);
        public Task<BaseResponse> DeactivateSoilTypeStatusAsync(Guid id);

    }
}
