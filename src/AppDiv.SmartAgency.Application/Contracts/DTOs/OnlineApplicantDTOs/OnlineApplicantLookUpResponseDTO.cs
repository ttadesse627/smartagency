using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs
{
    public class OnlineApplicantLookUpResponseDTO
    {
        public Guid Id { get; set; }
        public string? Value { get; set; }
    }
}