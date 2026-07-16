using CAS.CASDbContext;
using CAS.Interfaces.Repositories;

namespace CAS.Implementation.Repositories
{
    public class CropRepository(CASContext context) : BaseRepository(context), ICropRepository
    {
    }
}
