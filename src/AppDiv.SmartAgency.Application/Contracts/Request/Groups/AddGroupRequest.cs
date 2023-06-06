using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Groups
{
    public class AddGroupRequest
    {
        public string GroupName { get; set; }
        public Dictionary<string, List<string>> Description { get; set; }
        public List<string> Roles { get; set; }
    }
}