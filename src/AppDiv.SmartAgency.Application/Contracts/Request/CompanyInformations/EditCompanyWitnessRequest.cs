using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations
{
    public class EditCompanyWitnessRequest: CompanyWitnessRequest
    {
         public Guid Id { get; set; }
         //public Guid? CompanyInformationId { get; set; }

    }
}