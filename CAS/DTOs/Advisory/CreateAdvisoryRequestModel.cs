namespace CAS.DTOs.Advisory
{
    public class CreateAdvisoryRequestModel
    {
        public Guid CropId { get; set; }
        public Guid SoilTypeId { get; set; }
        public Guid SeasonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Location { get; set; }
    }

    public class CreateBulkAdvisoriesRequest
    {
        public List<CreateAdvisoryRequestModel> Advisories { get; set; } = new();
    }
}
