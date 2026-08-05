using CAS.DTOs.Advisory;
using CAS.Models.Entities;

namespace CAS.Interfaces.Repositories
{
    public interface IAdvisoryRepository : IBaseRepository
    {
        Task<PagedResult<Advisory>> SearchAsync(
        string? keyword,
        Guid? cropId,
        Guid? seasonId,
        Guid? soilTypeId,
        int page,
        int pageSize);

        Task<Advisory?> GetByIdAsync(Guid id);
    }
}
