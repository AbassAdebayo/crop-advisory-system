using CAS.Models.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CAS.Models.Entities
{
    public class SaveGuide
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AdvisoryId { get; set; }
        public DateTime CreatedAt { get; set;}

    }
}
