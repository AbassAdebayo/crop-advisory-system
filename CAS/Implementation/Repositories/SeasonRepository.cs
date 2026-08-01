using CAS.CASDbContext;
using CAS.Interfaces.Repositories;
using CAS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAS.Implementation.Repositories
{
    public class SeasonRepository(CASContext context) : BaseRepository(context), ISeasonRepository
    {
        private readonly CASContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public async Task<List<Season>> GetAllSeasonsForAdmin()
        {
            return await _context.Seasons
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Season> GetSeasonByIdForAdmin(Guid id)
        {
#pragma warning disable
            return await _context.Seasons
                .IgnoreQueryFilters()
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == id);
#pragma warning restore
        }
    }
}
