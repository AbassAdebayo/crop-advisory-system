using CAS.CASDbContext;
using CAS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CAS.Implementation.Repositories
{
    public class UnitOfWork(CASContext context) : IUnitOfWork
    {
        private readonly CASContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _context.Database.CreateExecutionStrategy();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
