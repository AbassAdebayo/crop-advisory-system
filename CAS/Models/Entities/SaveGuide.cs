using CAS.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CAS.Models.Entities
{
    public class SaveGuide : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid AdvisoryId { get; set; }
        public User User { get; set; } = default!;
        public Advisory Advisory { get; set; } = default!;

    }
}
