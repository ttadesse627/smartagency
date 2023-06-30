using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs
{
    public class NotAssignedVisaResponseDTO
    {
        public string AgencyName {get; set;}
        public string OrderNumber {get; set;}
        public string VisaNumber {get; set;}
        public int Duration {get; set;}
        public string JobTitle {get; set;}
        public string Sponsor {get; set;}
        public int Age {get; set;}
        public string Language {get; set;}
        public string Expereince {get; set;}
        public int NoOfVisa {get; set;}
        public string Religion {get; set;}

    }
}