using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs
{
    public record GroupDTO
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public JObject Description { get; set; }
        public JArray Roles { get; set; }
    }
}