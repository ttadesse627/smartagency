using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Groups
{
    public class AddGroupRequest
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<PermissionRequest> Permissions { get; set; } = [];
    }
}