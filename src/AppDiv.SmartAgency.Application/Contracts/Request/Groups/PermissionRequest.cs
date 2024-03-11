

using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Groups;
public record PermissionRequest
{
    public string Name { get; set; } = string.Empty;
    public List<PermissionEnum> Actions { get; set; } = [];
}