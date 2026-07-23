using CAS.Models.Entities;

namespace CAS.Interfaces.Repositories
{
    public interface ICropRepository : IBaseRepository
    {
        Task<IReadOnlyList<Crop>> GetAllCropsForAdmin();
    }
}
