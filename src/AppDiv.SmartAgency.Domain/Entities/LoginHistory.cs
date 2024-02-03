

namespace AppDiv.SmartAgency.Domain.Entities;
public class LoginHistory
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public string EventType { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public string IpAddress { get; set; } = null!;
    public string Device { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}