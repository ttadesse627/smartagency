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

        public Guid? Id {get; set;}
        public int? AmountPerPerson {get; set;}
       // public Guid CompanyInformationId {get; set;}
        public LookUpItemResponseDTO LookUpCountryOperation {get; set;}

    }
}