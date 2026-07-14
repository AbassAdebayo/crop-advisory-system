using System.ComponentModel.DataAnnotations;

namespace CAS.DTOs.Auth
{
    public class RegisterFarmerRequestModel
    {
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required string PasswordHash { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Location { get; set; }
    }
}
