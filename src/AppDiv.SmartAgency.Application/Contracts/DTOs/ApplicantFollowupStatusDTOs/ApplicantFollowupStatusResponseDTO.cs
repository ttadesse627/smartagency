using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusDTOs
{
    public class ApplicantFollowupStatusResponseDTO
    {
        public Guid Id { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
        public string? Remark { get; set; }

        public DepositApplicantResponseDTO? Applicant { get; set; }

        public OnlineApplicantLookUpResponseDTO? FollowupStatus { get; set; }


    }
}