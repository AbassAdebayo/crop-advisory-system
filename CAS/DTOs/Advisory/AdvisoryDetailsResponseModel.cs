namespace CAS.DTOs.Advisory
{
    public class AdvisoryDetailsResponseModel
    {
        public Guid Id { get; set; }

        public string Crop { get; set; } = default!;

        public string Season { get; set; } = default!;

        public string SoilType { get; set; } = default!;

        public string Location { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string WateringAdvice { get; set; } = default!;

        public string FertilizerAdvice { get; set; } = default!;

        public string PestControlAdvice { get; set; } = default!;

        public string HarvestingTips { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
        public bool IsFavourite { get; set; }
    }
}
