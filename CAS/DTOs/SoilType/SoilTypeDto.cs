using CAS.Contracts.Enums;

namespace CAS.DTOs.SoilType
{
    public class SoilTypeDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required Status SoilTypeStatus { get; set; }
    }

    public class SoilTypeListResponseModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required Status SoilTypeStatus { get; set; }
    }
}
