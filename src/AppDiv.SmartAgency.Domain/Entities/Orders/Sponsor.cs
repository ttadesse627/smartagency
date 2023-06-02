using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Orders;
public class Sponsor
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? FullNameAmharic { get; set; }
    public string? FullNameArabic { get; set; }
    public string? OtherName { get; set; }
    public string? ResidentialTitle { get; set; }
    public int NumberOfFamily { get; set; }

    // Foreign Keys
    public Guid? OrderId { get; set; }
    public Guid? AttachmentFileId { get; set; }
    public Guid? AddressId { get; set; }


    // Navigation properties
    public AttachmentFile? AttachmentFile { get; set; }
    public Order? Order { get; set; }
    public Address? Address { get; set; }
    public Enjaz? Enjaz { get; set; }

}