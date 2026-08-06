using CAS.CASDbContext;
using CAS.Interfaces.Repositories;
using CAS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAS.Implementation.Repositories
{
    public class SaveGuideRepository(CASContext context) : BaseRepository(context), ISaveGuideRepository
    {
        private readonly CASContext _context = context;
        public async Task<IReadOnlyList<SaveGuide>> GetFavourites(Guid userId)
        {
            return await _context.SaveGuides
                .Include(s => s.Advisory)
                .ThenInclude(a => a.Crop)
                .Include(s => s.Advisory)
                .ThenInclude(a => a.SoilType)
                .Include(s => s.Advisory)
                .ThenInclude(a => a.Season)
                .OrderByDescending(s => s.CreatedAt)
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
        }
    }
}
