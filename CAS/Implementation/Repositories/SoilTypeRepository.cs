using CAS.CASDbContext;
using CAS.Interfaces.Repositories;
using CAS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAS.Implementation.Repositories
{
    public class SoilTypeRepository(CASContext context) : BaseRepository(context), ISoilTypeRepository
    {
        private readonly CASContext _Context = context ?? throw new ArgumentNullException(nameof(context));
        public async Task<List<SoilType>> GetAllSoilTypesForAdmin()
        {
            return await _Context.SoilTypes
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SoilType> GetSoilTypeByIdForAdmin(Guid id)
        {
#pragma warning disable 
            return await _Context.SoilTypes
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(s => s.Id == id);
#pragma warning restore
        }
    }
}
