using AppDiv.SmartAgency.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Otp { get; set; }
        public DateTime? OtpExpiredDate { get; set; }
        public string? PasswordResetOtp { get; set; }
        public DateTime? PasswordResetOtpExpiredDate { get; set; }
        public string? FullName { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? PartnerId { get; set; }
        public Guid AddressId { get; set; }
        public LookUp? Position { get; set; }
        public LookUp? Branch { get; set; }
        public Partner? Partner { get; set; }
        public ICollection<Complaint>? Complaints { get; set; }
        public virtual Address? Address { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; } = [];
        public virtual required ICollection<LoginHistory> LoginHistories { get; set; }

    }
}
