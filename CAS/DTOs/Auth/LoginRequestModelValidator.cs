using FluentValidation;

namespace CAS.DTOs.Auth
{
    public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Enter valid email address")
                .Equal(x => x.Email.Trim()).WithMessage("Email cannot contain whiotespace");

            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("Password is required.");
        }
    }
}
