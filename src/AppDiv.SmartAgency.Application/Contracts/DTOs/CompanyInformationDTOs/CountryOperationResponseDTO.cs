using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs
{
    public class CountryOperationResponseDTO
    {

        //public Guid? Id {get; set;}
        public LookUpItemResponseDTO? Country{get; set;}
       public string? LicenseNumber {get; set;}
        public int VisaExpiryDays {get; set;}

    }
}