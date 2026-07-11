using CAS.CASDbContext;
using CAS.Interfaces.Repositories;

namespace CAS.Implementation.Repositories
{
    public class RoleRepository(CASContext context) : BaseRepository(context), IRoleRepository
    {
    }
}
