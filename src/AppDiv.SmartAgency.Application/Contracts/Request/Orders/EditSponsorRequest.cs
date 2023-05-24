
using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record EditSponsorRequest
{
    public Guid Id { get; set; }
    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? FullNameAmharic { get; set; }
    public string? FullNameArabic { get; set; }
    public string? OtherName { get; set; }
    public string? ResidentialTitle { get; set; }
    public int NumberOfFamily { get; set; }
    public EditAttachmentFileRequest? AttachmentFile { get; set; }
    public EditAddressRequest? Address { get; set; }
}