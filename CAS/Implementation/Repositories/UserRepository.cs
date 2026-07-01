using CAS.CASDbContext;
using CAS.Interfaces.Repositories;

namespace CAS.Implementation.Repositories
{
    public class UserRepository(CASContext context) :  BaseRepository(context), IUserRepository
    {

    }
}
