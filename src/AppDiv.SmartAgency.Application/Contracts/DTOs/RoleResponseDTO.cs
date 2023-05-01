using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs
{
    public record RoleResponseDTO
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
    }
}
