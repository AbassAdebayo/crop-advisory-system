using CAS.CASDbContext;
using CAS.Interfaces.Repositories;

namespace CAS.Implementation.Repositories
{
    public class AdvisoryRepository(CASContext context) : BaseRepository(context), IAdvisoryRepository
    {
    }
}
