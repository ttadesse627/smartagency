using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants
{
    public class OnlineApplicantRequest
    {
  
      public string  FullName {get; set;}
      public string Passport {get; set;} 
        public string Sex {get; set;} 
        public string Age {get; set;} 
        
        public string Region{get; set;}
        public string  City{get; set;} 
        public string  PhoneNumber{get; set;}
        public string EducationLevel{get; set;}
        public Guid DesiredCountryId{get; set;}
        public Guid  MaritalStatusId {get; set;}
        public Guid  ExperienceId{get; set;} 
    }
}