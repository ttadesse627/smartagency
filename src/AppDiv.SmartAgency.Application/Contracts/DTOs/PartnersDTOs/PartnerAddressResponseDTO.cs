using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs
{
    public class PartnerAddressResponseDTO
    {

        public Guid? Id { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? DistrictArabic { get; set; }
        public string? Street { get; set; }
         public string? StreetArabic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }

        public  OnlineApplicantLookUpResponseDTO? Country{get; set;}

        
    }
}