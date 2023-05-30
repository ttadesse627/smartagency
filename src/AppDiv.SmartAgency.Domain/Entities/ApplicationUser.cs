
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? PartnerId { get; set; }
        public LookUp? Position { get; set; }
        public LookUp? Branch { get; set; }
        public Partner? Partner { get; set; }
    }
}
