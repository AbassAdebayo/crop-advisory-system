using CAS.Models.Entities;

namespace CAS.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository
    {

        Task<User> GetRoleByUser(Guid userId);
    }
}
