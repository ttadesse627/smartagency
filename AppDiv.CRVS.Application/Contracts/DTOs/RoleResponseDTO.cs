using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Application.Contracts.DTOs
{
    public record RoleResponseDTO
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
    }
}
