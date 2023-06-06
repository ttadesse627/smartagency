using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;

namespace AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations
{
    public class CompanyWitnessRequest
    {
     public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
        
    }
}