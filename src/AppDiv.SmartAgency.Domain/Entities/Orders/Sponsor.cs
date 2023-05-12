using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Orders;
public class Sponsor
{
    public Guid Id { get; set; }
    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? FullNameAmharic { get; set; }
    public string? FullNameArabic { get; set; }
    public string? OtherName { get; set; }
    public string? ResidentialTitle { get; set; }
    public int NumberOfFamily { get; set; }

    // Foreign Keys
    public Guid? SponsorIDFileId { get; set; }
    public Guid? SponsorAddressId { get; set; }


    // Navigation properties
    public FileCollection? SponsorIDFile { get; set; }
    public Order? SponsorOrder { get; set; }
    public Address? SponsorAddress { get; set; }

}