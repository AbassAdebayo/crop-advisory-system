using CAS.CASDbContext;
using CAS.Interfaces.Repositories;
using CAS.Models.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CAS.Implementation.Repositories
{
    public class BaseRepository(CASContext context) : IBaseRepository
    {
        
        private readonly CASContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public async Task<T> Add<T>(T entity) where T : BaseEntity
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public async Task<bool> Any<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
#pragma warning disable CS8603 
            return await _context.Set<T>().Where(expression).SingleOrDefaultAsync();
#pragma warning restore CS8603
        }

        public async Task<IReadOnlyList<T>> GetAll<T>() where T : BaseEntity
        {
            return await _context.Set<T>()
               .AsNoTracking()
               .ToListAsync();
        }

        public IQueryable<T> QueryWhere<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return _context.Set<T>()
                 .Where(expression);
        }

        public async Task<T> Update<T>(T entity) where T : BaseEntity
        {
            _context.Update<T>(entity);
            return entity;
        }
    }
}
