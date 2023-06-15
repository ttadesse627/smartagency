using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Groups
{
    public class AddGroupRequest
    {
        public string GroupName { get; set; }
        public string Description { get; set; }
        public JArray Roles { get; set; }
    }
}