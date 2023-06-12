using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Domain.Entities.Base;
public class Address : BaseAuditableEntity
{
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? SubCity { get; set; }
    public string? SubCityArabic { get; set; }
    public string? District { get; set; }
    public string? DistrictArabic { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public string? WoredaArabic { get; set; }
    public string? Kebele { get; set; }
    public string? Street { get; set; }
    public string? StreetArabic { get; set; }
    public string? PhoneNumber { get; set; }
    public string? HouseNumber { get; set; }
    public string? OfficePhone { get; set; }
    public string? Mobile { get; set; }
    public string? AlternativePhone { get; set; }
    public string? Fax { get; set; }
    public string Adress { get; set; } = string.Empty;
    public string? PostCode { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }

    // Foreign Keys
    public Guid? AddressRegionId { get; set; }

    public Guid? CountryId { get; set; }
    // Navigation properties

    public LookUp? Country { get; set; }
    public LookUp? AddressRegion { get; set; }
    public Applicant? Applicant { get; set; }
    public EmergencyContact? EmergencyContact { get; set; }
    public Partner? Partner { get; set; }
    public Sponsor? Sponsor { get; set; }
    public CompanyInformation? CompanyInformation { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }




}