using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs
{
    public class GetUnAssignedOrdersDropdownResponseDTO
    {
         public Guid? OrderId{get; set;}
        public string? OrderNumber{get; set;}
        public string? SponsorName{get; set;}
        public string? VisaNumber{get; set;}
        public string? JobTitle{get; set;}

    }
}