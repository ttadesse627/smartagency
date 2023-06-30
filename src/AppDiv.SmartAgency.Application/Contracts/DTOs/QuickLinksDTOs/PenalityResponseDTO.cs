using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs
{
    public class PenalityResponseDTO
    {
        public string Customer {get; set;}
        public string Sponsor {get; set;}
        public int Days {get; set;}
        public int Penality {get; set;}
    }
}