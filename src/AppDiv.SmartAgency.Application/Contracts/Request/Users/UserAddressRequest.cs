

namespace AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
public record UserAddressRequest
{
    public string? City { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public string? Kebele { get; set; }
    public string? PhoneNumber { get; set; }
    public string? HouseNumber { get; set; }
    public string Adress { get; set; }
    public string? Website { get; set; }
    public Guid? AddressRegionId { get; set; }
}