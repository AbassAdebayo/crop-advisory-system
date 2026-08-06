using CAS.DTOs;
using CAS.DTOs.Advisory;
using CAS.Models.Entities;

namespace CAS.Interfaces.Services
{
    public interface IAdvisoryService
    {
        Task<BaseResponse> CreateAdvisoryAsync(List<CreateAdvisoryRequestModel> request);
        Task<BaseResponse<PagedResponse<AdvisoryCardResponseModel>>> SearchAsync(SearchAdvisoryRequestModel request);

        Task<BaseResponse> ToggleFavouriteAsync(Guid farmerId, Guid advisoryId);

       // Task<BaseResponse<IReadOnlyList<AdvisoryCardResponseModel>>> GetFavouritesAsync(Guid userId);
        Task<BaseResponse<IReadOnlyList<AdvisoryCardResponseModel>>> GetFavouriteAdvisoriesAsync(Guid userId);
        Task<BaseResponse<AdvisoryDetailsResponseModel>> GetFavouriteAdvisoryDetails(Guid advisoryId, Guid userId);
    }
}
