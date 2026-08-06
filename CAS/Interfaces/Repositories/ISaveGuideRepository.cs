using CAS.Models.Entities;

namespace CAS.Interfaces.Repositories
{
    public interface ISaveGuideRepository : IBaseRepository
    {
        Task<IReadOnlyList<SaveGuide>> GetFavourites(Guid userId);
    }
}
