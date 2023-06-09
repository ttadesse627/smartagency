using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs
{
    public class GetPartnerDropDownDTO
    {
        public Guid Id { get; set; }
        public string? PartnerName { get; set; }
        public string? OrderNumber { get; set; }
    }
}