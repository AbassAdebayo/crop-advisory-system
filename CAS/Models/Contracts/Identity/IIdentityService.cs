using CAS.Models.Entities;

namespace CAS.Models.Contracts.Identity
{
    public interface IIdentityService
    {
        public string GetPasswordHash(string password, string salt = null!);
        bool VerifyPassword(string hashedPassword, string providedPassword);
        public Task<string> GetRoleAsync(User user);
    }
}
