
using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record SponsorRequest
{
    public string? IdNumber { get; set; }
    public string? FullName { get; set; }
    public string? FullNameAmharic { get; set; }
    public string? OtherName { get; set; }
    public string? ResidentialTitle { get; set; }
    public int NumberOfFamily { get; set; }
    public FileCollectionRequest? SponsorIDFile { get; set; }
    public AddressRequest? SponsorAddress { get; set; }
}