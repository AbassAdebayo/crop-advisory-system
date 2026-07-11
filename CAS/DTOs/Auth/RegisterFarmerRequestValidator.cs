using FluentValidation;

namespace CAS.DTOs.Auth
{
    public class RegisterFarmerRequestValidator : AbstractValidator<RegisterFarmerRequestModel>
    {
        public RegisterFarmerRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Fullname is required")
                .MinimumLength(4).WithMessage("The minimum length of fullname is 4 characters")
                .MaximumLength(100).WithMessage("The maximum length of fullname is 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Enter valid email address")
                .Equal(x => x.Email.Trim()).WithMessage("Email cannot contain whiotespace");

            RuleFor(x => x.PasswordHash)
                 .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(50).WithMessage("Password must not exceed 50 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");


            RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.PasswordHash).WithMessage("Confirm password must match the password.");

            RuleFor(x => x.PhoneNumber)
               .NotEmpty().WithMessage("Phone number is required.")
               .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be a valid international phone number.");

        }
    }
}
