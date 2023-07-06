using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs
{
    public class DynamicProcessResponseDTO
    {

        
        public string? ProcessDefnitionName {get; set;}
        public string? ApplicantName {get; set;}
        public string? PassportNumber {get; set;}
        public int DatePassed {get; set;}
    }
}