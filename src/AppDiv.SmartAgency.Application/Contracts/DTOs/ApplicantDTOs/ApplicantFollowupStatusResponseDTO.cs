using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs
{
    public class ApplicantFollowupStatusResponseDTO
    {
        
        public Guid Id {get; set;}
        public string PassportNumber {get; set;} 
        public DateTime Month {get; set;}  
        public string Remark {get; set;} 
        //public  Guid ApplicantId {get; set;} 
        public DepositApplicantResponseDTO Applicant {get; set;}

        public  OnlineApplicantLookUpResponseDTO  FollowupStatus {get; set;}


    }
}