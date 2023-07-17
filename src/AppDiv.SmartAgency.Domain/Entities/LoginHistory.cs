

namespace AppDiv.SmartAgency.Domain.Entities;
public class LoginHistory
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string EventType { get; set; }
    public DateTime EventDate { get; set; }
    public string IpAddress { get; set; }
    public string Device { get; set; }
    public ApplicationUser User { get; set; }
}