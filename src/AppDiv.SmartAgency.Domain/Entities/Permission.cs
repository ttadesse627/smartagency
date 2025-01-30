using AppDiv.SmartAgency.Domain.Enums;
using Twilio.Base;

namespace AppDiv.SmartAgency.Domain.Entities;
public class Permission
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid GroupId { get; set; }
    public required UserGroup Group { get; set; }
    public Guid ResourceId { get; set; }
    public required Resource Resource { get; set; }
    public List<PermissionEnum> Actions { get; set; } = [];
}