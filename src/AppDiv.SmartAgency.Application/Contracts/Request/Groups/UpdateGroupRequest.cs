using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Groups;
public class UpdateGroupRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<PermissionRequest> Permissions { get; set; } = [];

}
