using AppDiv.SmartAgency.Domain.Enums;
using Twilio.Base;

namespace AppDiv.SmartAgency.Domain.Entities;
public class Permission
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid UserGroupId { get; set; }
    public UserGroup UserGroup { get; set; } = null!;
    public Guid ResourceId { get; set; }
    public Resource Resource { get; set; } = null!;
    public List<PermissionEnum> Actions { get; set; } = [];
}