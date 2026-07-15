using CAS.Models.Entities;

namespace CAS.Contracts.Identity
{
    public interface IIdentityService
    {
        public string GetPasswordHash(string password, string salt = null!);
        bool VerifyPassword(string hashedPassword, string providedPassword);
        public Task<string> GetRoleAsync(User user);
        public string GetNormalizedEmail(string email);

    }
}
