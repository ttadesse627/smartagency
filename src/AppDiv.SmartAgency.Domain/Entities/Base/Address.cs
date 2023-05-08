using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Domain.Entities.Base
{
    public class Address : BaseAuditableEntity
    {
        public string? Region { get; set; }
        public string? Zone { get; set; }
        public string? Woreda { get; set; }
        public string? Kebele { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }

        // Foreign Keys
        public Guid? AddressCountryId { get; set; }

        // Navigation properties
        public LookUp? AddressCountry { get; set; }
        public Applicant? AddressApplicant { get; set; }
        public EmergencyContact? AddressEmergencyContact { get; set; }
        public Repersentative? AddressRepresentative { get; set; }
        public Partner? AddressPartner { get; set; }
        public Sponsor? AddressSponsor { get; set; }
    }
}