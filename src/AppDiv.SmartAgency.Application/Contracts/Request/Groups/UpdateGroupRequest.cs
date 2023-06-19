using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Groups;
public class UpdateGroupRequest
{
    public Guid Id { get; set; }
    public string GroupName { get; set; }
    public string Description { get; set; }
    public JArray Roles { get; set; }

}
