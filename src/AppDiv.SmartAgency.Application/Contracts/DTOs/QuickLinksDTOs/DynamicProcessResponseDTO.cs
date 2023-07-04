using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs
{
    public class DynamicProcessResponseDTO
    {
        public string ApplicantName {get; set;}
        public string PassportNumber {get; set;}
        public string Status {get; set;}
        public int DatePassed {get; set;}
    }
}