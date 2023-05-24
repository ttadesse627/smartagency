using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs
{
    public class GetCompanyInformationResponseDTO
    {
        public Guid Id {get; set;}
         public string CompanyName { get; set; }

        public string CompanyNameAmharic { get; set; }

        public string CompanyNameArabic { get; set; }

        public string? ContractNumber{ get; set; }

       public string?  licenseNumber { get; set; }
       public string AssurancePolicyNumber { get; set; }

       public string GeneralManager { get; set; }
       public string? GeneralManagerAmharic { get; set; }
       public string? ViceManager { get; set; }
       public string? ViceManagerAmharic { get; set; }
       public string? CountriesOperation { get; set; }
       public string? LetterLogo { get; set; }
       public string? LetterBackGround{ get; set; }
       
       public Guid? AddressId { get; set; }
       public CompanyAddressResponseDTO? Address { get; set; }
       public ICollection<WitnessRequest>? Witnesses { get; set; }
       public CompanySettingRequest? CompanySetting {get; set;} 
       public ICollection<CountryOperationResponseDTO>? CountryOperations { get; set; }
    }
}