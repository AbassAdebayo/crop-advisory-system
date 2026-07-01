using CAS.Models.Contracts;

namespace CAS.Models.Entities
{
    public class User : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Location { get; set; }
    }
}
