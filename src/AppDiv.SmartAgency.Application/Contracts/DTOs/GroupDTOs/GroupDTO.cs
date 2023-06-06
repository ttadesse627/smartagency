using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs
{
    public record GroupDTO
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public Dictionary<string, List<string>> Description { get; set; }
        public List<string> Roles { get; set; }
    }
}