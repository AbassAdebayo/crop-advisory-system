using CAS.CASDbContext;
using CAS.Interfaces.Repositories;
using CAS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAS.Implementation.Repositories
{
    public class UserRepository(CASContext context) : BaseRepository(context), IUserRepository
    {
        private readonly CASContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public async Task<User> GetRoleByUser(Guid userId)
        {
#pragma warning disable CS8603 
            return await _context.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Id == userId);
#pragma warning restore CS8603



        }
    }
}
