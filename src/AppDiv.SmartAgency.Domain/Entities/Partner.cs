using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class Partner : BaseAuditableEntity
    {
        public string? PartnerType { get; set; }
        public string? PartnerName { get; set; }
        public string? PartnerNameAmharic { get; set; }
        public string? PartnerNameArabic { get; set; }
        public string? ContactPerson { get; set; }
        public string? IdNumber { get; set; }
        public string? ManagerNameAmharic { get; set; }
        public string? LicenseNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankAccount { get; set; }
        //public string? HeaderLogo { get; set; }
        public string? ReferenceNumber { get; set; }

        // Foreign Keys
        public Guid? AddressId { get; set; }

        // Navigation properties
        public Address? Address { get; set; }
        public ICollection<RequestedApplicant>? RequestedApplicants { get; set; }
        public ICollection<Applicant>? Applicants { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<ApplicationUser>? Users { get; set; }


    }
}