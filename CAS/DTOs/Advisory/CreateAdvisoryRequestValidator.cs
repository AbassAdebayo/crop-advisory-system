using FluentValidation;

namespace CAS.DTOs.Advisory
{
    public class CreateAdvisoryRequestValidator : AbstractValidator<CreateAdvisoryRequestModel>
    {
        public CreateAdvisoryRequestValidator()
        {
            RuleFor(x => x.CropId)
                .NotEmpty().WithMessage("Crop is required.");

            RuleFor(x => x.SoilTypeId)
                .NotEmpty().WithMessage("Soil type is required.");

            RuleFor(x => x.SeasonId)
                .NotEmpty().WithMessage("Season is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        }
    }
}
