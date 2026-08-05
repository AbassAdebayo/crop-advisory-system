using CAS.DTOs;
using CAS.DTOs.Advisory;

namespace CAS.Interfaces.Services
{
    public interface IAdvisoryService
    {
        Task<BaseResponse> CreateAdvisoryAsync(List<CreateAdvisoryRequestModel> request);
        Task<BaseResponse<PagedResponse<AdvisoryCardResponseModel>>> SearchAsync(SearchAdvisoryRequestModel request);
        Task<BaseResponse<AdvisoryDetailsResponseModel>> GetDetailsAsync(Guid id);
    }
}
