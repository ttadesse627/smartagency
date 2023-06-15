
namespace AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
public class UpdateAddressRequest
{
    public Guid Id { get; set; }
    public string Zone { get; set; }
    public string Woreda { get; set; }
    public string Kebele { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public Guid RegionId { get; set; }
}