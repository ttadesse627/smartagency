
using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record EditSponsorRequest
{
    public Guid Id { get; set; }
    public string? IdNumber { get; set; }
    public string? FullName { get; set; }
    public string? FullNameAmharic { get; set; }
    public string? OtherName { get; set; }
    public string? ResidentialTitle { get; set; }
    public int NumberOfFamily { get; set; }
    public EditAttachmentFileRequest? SponsorIDFile { get; set; }
    public EditAddressRequest? SponsorAddress { get; set; }
}