using CAS.Contracts;
using CAS.Contracts.Enums;

namespace CAS.Models.Entities
{
    public class Advisory : BaseEntity
    {
        public Guid CropId { get; set; }
        public Guid SoilTypeId { get; set; }
        public Guid SeasonId { get; set; }
        public required string Location { get; set; }
        public required string Title { get; set; }
        public required string PlantingTips { get; set; }
        public required string WateringAdvice { get; set; }
        public required string FertilizerAdvice { get; set; }
        public required string PestControlAdvice { get; set; }
        public required string HarvestingTips { get; set; }
        public required Status Advisorytatus { get; set; }


    }
}

