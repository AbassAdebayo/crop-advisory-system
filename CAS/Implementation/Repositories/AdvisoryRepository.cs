using CAS.CASDbContext;
using CAS.DTOs.Advisory;
using CAS.Interfaces.Repositories;
using CAS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAS.Implementation.Repositories
{
    public class AdvisoryRepository(CASContext context) : BaseRepository(context), IAdvisoryRepository
    {
        private readonly CASContext _context = context;
        public async Task<Advisory?> GetByIdAsync(Guid id)
        {
            return await _context.Advisories
                .Include(x => x.Crop)
                .Include(x => x.Season)
                .Include(x => x.SoilType)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<Advisory>> SearchAsync(string? keyword, Guid? cropId, Guid? seasonId, Guid? soilTypeId, int page, int pageSize)
        {
            var query = _context.Advisories
            .Include(x => x.Crop)
            .Include(x => x.Season)
            .Include(x => x.SoilType)
            .AsNoTracking()
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();

                query = query.Where(x =>
                    x.Title.Contains(keyword) ||
                    x.Crop.Name.Contains(keyword));
            }

            if (cropId.HasValue)
                query = query.Where(x => x.CropId == cropId);

            if (seasonId.HasValue)
                query = query.Where(x => x.SeasonId == seasonId);

            if (soilTypeId.HasValue)
                query = query.Where(x => x.SoilTypeId == soilTypeId);

            var total = await query.CountAsync();

            var data = await query
                .OrderBy(x => x.Crop.Name)
                .ThenBy(x => x.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Advisory>
            {
                Items = data,
                TotalRecords = total,
                CurrentPage = page,
                PageSize = pageSize
            };
        }
    }
}
