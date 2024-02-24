

namespace AppDiv.SmartAgency.Domain.Entities;
public class GroupRole
{
    public Guid Id { get; set; }
    public string Page { get; set; } = null!;
    public bool CanAdd { get; set; }
    public bool CanView { get; set; }
    public bool CanViewDetail { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanDelete { get; set; }
}