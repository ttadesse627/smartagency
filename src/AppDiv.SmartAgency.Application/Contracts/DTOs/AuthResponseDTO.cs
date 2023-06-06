using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.RoleDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs
{
    public record AuthResponseDTO
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string FullName { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
