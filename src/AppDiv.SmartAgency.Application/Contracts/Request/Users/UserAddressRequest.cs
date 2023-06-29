

namespace AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
public record UserAddressRequest
{
    public string? SubCity { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public string? Kebele { get; set; }
    public string? PhoneNumber { get; set; }
    public string? HouseNumber { get; set; }
    public string Adress { get; set; }
    public string? Website { get; set; }
    public Guid? RegionId { get; set; }
}