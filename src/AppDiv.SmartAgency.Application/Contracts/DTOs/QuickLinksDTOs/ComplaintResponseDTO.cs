using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs
{
    public class ComplaintResponseDTO
    {
        public string Sponsor {get; set;}
        public string Employee {get; set;}
        public int Days {get; set;}

        public string Path {get; set;}
    }
}