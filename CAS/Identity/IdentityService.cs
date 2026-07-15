using CAS.Contracts.Identity;
using CAS.Interfaces.Repositories;
using CAS.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace CAS.Identity
{
    public class IdentityService(IPasswordHasher<User> passwordHasher, IConfiguration config,
        IUserRepository userRepository) : IIdentityService
    {

        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        IConfiguration _config = config ?? throw new ArgumentNullException(nameof(config));

        public string GetPasswordHash(string password, string salt = null!)
        {
            if (string.IsNullOrEmpty(salt))
            {
                return _passwordHasher.HashPassword(new User(), password);
            }
            return _passwordHasher.HashPassword(new User(), $"{password}{salt}");
        }

        public async Task<string> GetRoleAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var role = await _userRepository.GetRoleByUser(user.Id);

            if (role is null) throw new ArgumentNullException(nameof(role));

            return role?.Role?.Name != null ? new string(role.Role.Name) : string.Empty;
        }


        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(new User(), hashedPassword, providedPassword);
            Console.WriteLine($"Password verification result: {result}");
            return result == PasswordVerificationResult.Success;
        }
        
        public string GetNormalizedEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            return email.ToLower();
        }
    }
}
