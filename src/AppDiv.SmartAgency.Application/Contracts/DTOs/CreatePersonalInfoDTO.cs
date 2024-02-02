using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs
{
    public record CreatePersonalInfoDTO
    {
        public required string FirstName { get; set; }
    }
}
