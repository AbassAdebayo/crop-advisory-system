using CAS.Contracts.Enums;

namespace CAS.DTOs.Crop
{
    public class CreateCropRequestModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string ImageUrl { get; set; }
    }
}
