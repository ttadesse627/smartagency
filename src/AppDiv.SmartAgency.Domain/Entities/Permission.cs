using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities;
public class Permission
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public List<PermissionEnum> Actions { get; set; } = [];
}