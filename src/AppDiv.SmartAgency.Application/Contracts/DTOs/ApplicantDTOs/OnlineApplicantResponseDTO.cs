using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
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
        public  OnlineApplicantLookUpResponseDTO? DesiredCountry{get; set;}
        public  OnlineApplicantLookUpResponseDTO?  MaritalStatus {get; set;}
        public OnlineApplicantLookUpResponseDTO?  Experience{get; set;}      
    }
}