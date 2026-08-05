namespace CAS.DTOs.Advisory
{
    public class AdvisoryCardResponseModel
    {
        public Guid Id { get; set; }

        public string Crop { get; set; } = default!;

        public string Season { get; set; } = default!;

        public string SoilType { get; set; } = default!;

        public string Location { get; set; } = default!;

        public string Title { get; set; } = default!;

    }
}
