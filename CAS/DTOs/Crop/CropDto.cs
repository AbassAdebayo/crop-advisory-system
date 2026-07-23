using CAS.Contracts.Enums;

namespace CAS.DTOs.Crop
{
    public class CropDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string ImageUrl { get; set; }
        public required Status CropStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CropListResponseModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string ImageUrl { get; set; }
        public required Status CropStatus { get; set; }

    }
}
