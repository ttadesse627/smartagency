using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class CompanyInformation: BaseAuditableEntity
    {
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
       public Address? Address { get; set; }
      
      public ICollection<Witness>? Witnesses { get; set; }
      public ICollection<CountryOperation>? CountryOperations { get; set; }
      public CompanySetting? CompanySetting {get; set;} 



    }
}