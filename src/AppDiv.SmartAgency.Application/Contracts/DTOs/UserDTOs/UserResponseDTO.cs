
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs
{
    public record UserResponseDTO
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<GroupDTO>? UserGroups { get; set; }

    }
}
