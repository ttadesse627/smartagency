using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs
{
    public record AuthResponseDTO
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Token { get; set; }
        public string? FullName { get; set; }
        public List<PermissionDto> Permissions { get; set; } = [];
        public GetPartnerDropDownDTO? Partner { get; set; }
    }
}
