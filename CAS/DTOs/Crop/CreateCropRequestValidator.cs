using FluentValidation;

namespace CAS.DTOs.Crop
{
    public class CreateCropRequestValidator : AbstractValidator<CreateCropRequestModel>
    {
        public CreateCropRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Crop name is required")
                .MinimumLength(2).WithMessage("Crop name cannot be lesser than two characters.")
                .MaximumLength(30).WithMessage("Crop name cannot exceed 30 characters");

            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Crop image is required");
                
        }
    }
}
