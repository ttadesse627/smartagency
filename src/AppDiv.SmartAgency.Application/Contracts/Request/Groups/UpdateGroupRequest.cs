using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Groups;
public class UpdateGroupRequest
{
    public Guid Id { get; set; }
    public string GroupName { get; set; }
    public Dictionary<string, List<string>> Description { get; set; }
    public List<string> Roles { get; set; }

}
