

namespace AppDiv.SmartAgency.Domain.Entities;
public class RolePermission
{
    public Guid UserGroupId { get; set; } = Guid.NewGuid();
    public Guid PermissionId { get; set; } = Guid.NewGuid();
}