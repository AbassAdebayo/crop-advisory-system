using CAS.Contracts;
using CAS.Contracts.Enums;

namespace CAS.Models.Entities
{
    public class Advisory : BaseEntity
    {
        public Guid CropId { get; set; }
        public Crop Crop { get; set; } = default!;
        public Guid SoilTypeId { get; set; }
        public SoilType SoilType { get; set; } = default!;
        public Guid SeasonId { get; set; }
        public Season Season { get; set; } = default!;
        public string? Location { get; set; }
        public required string Title { get; set; }
        public string WateringAdvice { get; set; } = string.Empty;
        public string FertilizerAdvice { get; set; } = string.Empty;
        public string PestControlAdvice { get; set; } = string.Empty;
        public string HarvestingTips { get; set; } = string.Empty;
        public Status Advisorytatus { get; set; } = Status.Active;


    }
}

