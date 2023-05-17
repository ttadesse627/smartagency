using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Domain.Entities.Base
{
    public class Address : BaseAuditableEntity
    {
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? DistrictArabic { get; set; }
        public string? Zone { get; set; }
        public string? Woreda { get; set; }
        public string? Kebele { get; set; }
        public string? Street { get; set; }
        public string? StreetArabic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? HouseNumber { get; set; }
        public string? OfficePhone { get; set; }
        public string? Mobile { get; set; }
        public string? AlternativePhone { get; set; }
        public string? Fax { get; set; }
        public string Addres { get; set; }
        public string? PostCode { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }

        // Foreign Keys
        public Guid? AddressRegionId { get; set; }

        // Navigation properties
        public LookUp? AddressRegion { get; set; }
        public Applicant? AddressApplicant { get; set; }
        public EmergencyContact? AddressEmergencyContact { get; set; }
        public Representative? AddressRepresentative { get; set; }
        public Partner? AddressPartner { get; set; }
        public Sponsor? AddressSponsor { get; set; }
    }
}