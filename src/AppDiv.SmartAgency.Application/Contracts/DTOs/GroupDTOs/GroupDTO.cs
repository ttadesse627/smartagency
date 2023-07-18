using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs
{
    public record GroupDTO
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        // public string DescriptionStr { get; set; }
        public JArray Roles { get; set; }
    }
}