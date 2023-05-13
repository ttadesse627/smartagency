using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs
{
    public class GetApplicantFollowupStatusByIdResponseDTO : CreateApplicantFollowupStatusRequest
    {
        public Guid Id {get; set;} 
        public Guid ApplicantId {get; set;}
        

    }
}