using CAS.Contracts.Enums;

namespace CAS.DTOs.Season
{
    public class SeasonDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required Status SeasonStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SeasonListResponseModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required Status SeasonStatus { get; set; }
    }
}
