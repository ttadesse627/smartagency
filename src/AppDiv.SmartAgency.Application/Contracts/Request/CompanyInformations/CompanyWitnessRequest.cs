using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;

namespace AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations
{
    public class CompanyWitnessRequest: WitnessRequest
    {
        public Guid? CompanyInformationId { get; set; }
        
    }
}