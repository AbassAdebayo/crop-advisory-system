using CAS.DTOs;
using CAS.DTOs.Season;
using CAS.DTOs.SoilType;

namespace CAS.Interfaces.Services
{
    public interface ISeasonService
    {
        Task<BaseResponse<IReadOnlyList<SeasonListResponseModel>>> GetAllSeasonsForAdminAsync();
        Task<BaseResponse<IReadOnlyList<SeasonListResponseModel>>> GetAllActiveSeasonsAsync();

        Task<BaseResponse<SeasonDto>> GetSeasonForAdminAsync(Guid id);
        Task<BaseResponse<SeasonDto>> GetActiveSeasonById(Guid id);
    }
}
