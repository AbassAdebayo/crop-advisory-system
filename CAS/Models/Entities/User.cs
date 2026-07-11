using CAS.Models.Contracts;

namespace CAS.Models.Entities
{
    public class User : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Location { get; set; }
    }
}
