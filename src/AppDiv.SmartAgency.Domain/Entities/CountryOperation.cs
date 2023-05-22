using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class CountryOperation : BaseAuditableEntity
    {
        public Guid? CountryId {get; set;}
        public int? AmountPerPerson {get; set;}
        public Guid? CompanyInformationId {get; set;}

        public LookUp? LookUpCountryOperation {get; set;}
        public CompanyInformation? CompanyInformation {get; set;}

        
    }
}