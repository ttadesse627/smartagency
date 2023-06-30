using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations
{
    public class CountryOperationRequest
    {
        
        public Guid? CountryId {get; set;}
       public string? LicenseNumber {get; set;}
        public int VisaExpiryDays {get; set;}
    }
}