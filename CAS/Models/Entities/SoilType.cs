using CAS.Contracts;
using CAS.Contracts.Enums;

namespace CAS.Models.Entities
{
    public class SoilType : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required Status SoilTypeStatus { get; set; } = Status.Active;
        public ICollection<Advisory> Advisories { get; set; } = new List<Advisory>();


        public void ActivateSoilTypeStatus(Status newSoilTypeStatus)
        {
            SoilTypeStatus = newSoilTypeStatus;
        }

        public void DeactivateCropStatus(Status newSoilTypeStatus)
        {
            SoilTypeStatus = newSoilTypeStatus;
        }
    }
}
