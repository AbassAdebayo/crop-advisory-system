using CAS.Models.Entities;

namespace CAS.Interfaces.Repositories
{
    public interface ISeasonRepository : IBaseRepository
    {
        Task<List<Season>> GetAllSeasonsForAdmin();
        Task<Season> GetSeasonByIdForAdmin(Guid id);
    }
}
