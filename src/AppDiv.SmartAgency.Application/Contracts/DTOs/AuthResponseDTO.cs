using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.RoleDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs
{
    public record AuthResponseDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public List<RoleDto>? Roles { get; set; }
        public GetPartnerDropDownDTO? Partner { get; set; }
    }
}
