using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs
{
    public record AuthResponseDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public List<PermissionDto> Permissions { get; set; } = [];
        public GetPartnerDropDownDTO? Partner { get; set; }
    }
}
