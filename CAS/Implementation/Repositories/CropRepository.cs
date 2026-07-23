using CAS.CASDbContext;
using CAS.Interfaces.Repositories;
using CAS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAS.Implementation.Repositories
{
    public class CropRepository(CASContext context) : BaseRepository(context), ICropRepository
    {
        private readonly CASContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<IReadOnlyList<Crop>> GetAllCropsForAdmin()
        {
            return await _context.Crops
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
        }
    }

}
