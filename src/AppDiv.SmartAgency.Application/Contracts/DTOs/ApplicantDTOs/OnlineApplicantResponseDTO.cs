using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AddressDTOs;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs
{
    public class OnlineApplicantResponseDTO
    {
      
      public Guid Id {get; set;}   
      public string  FullName {get; set;}
      public string Passport {get; set;} 
        public string Sex {get; set;} 
        public string Age {get; set;} 
        
        public string Region{get; set;}
        public string  City{get; set;} 
        public string  PhoneNumber{get; set;}
        public string EducationLevel{get; set;}
        public Guid DesiredCountryId{get; set;}
        public Guid  MartialStatusId {get; set;}
        public Guid  ExperienceId{get; set;}      
    }
}