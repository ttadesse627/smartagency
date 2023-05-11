
namespace AppDiv.SmartAgency.Application.Contracts.Request.Common;
public class EditAddressRequest
{
    public Guid Id { get; set; }
    public Guid AddressCountryId { get; set; }
    public string? Region { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public string? Kebele { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
}