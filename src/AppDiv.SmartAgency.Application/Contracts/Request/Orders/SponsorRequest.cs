
using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record SponsorRequest
{
    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? FullNameAmharic { get; set; }
    public string? FullNameArabic { get; set; }
    public string? OtherName { get; set; }
    public string? ResidentialTitle { get; set; }
    public int NumberOfFamily { get; set; }
    public AttachmentFileRequest? AttachmentFile { get; set; }
    public AddressRequest? Address { get; set; }
}