namespace CAS.DTOs.Advisory
{
    public class SearchAdvisoryRequestModel
    {
        public string? Keyword { get; set; }

        public Guid? CropId { get; set; }

        public Guid? SeasonId { get; set; }

        public Guid? SoilTypeId { get; set; }

        public string? Location { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
