using CAS.Contracts;
using CAS.Contracts.Enums;

namespace CAS.Models.Entities
{
    public class Crop : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string ImageUrl { get; set; }
        public required Status CropStatus { get; set; }
        public ICollection<Advisory> Advisories { get; set; } = new List<Advisory>(); 



        public void ActivateCropStatus(Status newCropStatus)
        {
            CropStatus = newCropStatus;
        }

        public void DeactivateCropStatus(Status newCropStatus)
        {
            CropStatus = newCropStatus;
        }



    }
}
