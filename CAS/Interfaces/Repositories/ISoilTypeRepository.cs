using CAS.Models.Entities;

namespace CAS.Interfaces.Repositories
{
    public interface ISoilTypeRepository : IBaseRepository
    {
        Task<List<SoilType>> GetAllSoilTypesForAdmin();
        Task<SoilType> GetSoilTypeByIdForAdmin(Guid id);
    }
}
