using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.ApplicantFollowupStatuses;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusResponseDTOs
{
    public class GetApplicantFollowupStatusByIdResponseDTO : CreateApplicantFollowupStatusRequest
    {
        public Guid Id {get; set;} 
        public Guid ApplicantId {get; set;}
        

    }
}