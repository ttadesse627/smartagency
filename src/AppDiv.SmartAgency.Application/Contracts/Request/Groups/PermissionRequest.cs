

using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Groups;
public record PermissionRequest
{
    public Guid ResourceId { get; set; }
    public List<PermissionEnum> Actions { get; set; } = [];
}