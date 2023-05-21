using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ApplicantFollowupStatuses;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusResponseDTOs
{
    public class GetApplicantFollowupStatusByIdResponseDTO 
    {
        public string Id { get; set; }
        public string PassportNumber { get; set; }
        public DateTime Month { get; set; }
        public string Remark { get; set; }
        public OnlineApplicantLookUpResponseDTO FollowupStatus {get; set;} 
        public Guid ApplicantId {get; set;}
        

    }
}