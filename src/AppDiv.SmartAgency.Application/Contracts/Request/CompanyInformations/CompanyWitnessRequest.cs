using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;

namespace AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations
{
    public class CompanyWitnessRequest
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

    }
}