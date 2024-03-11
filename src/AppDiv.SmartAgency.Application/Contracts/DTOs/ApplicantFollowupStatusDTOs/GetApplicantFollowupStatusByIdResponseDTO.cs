using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusResponseDTOs
{
    public class GetApplicantFollowupStatusByIdResponseDTO
    {
        public Guid Id { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
        public DateTime? Month { get; set; }
        public string Remark { get; set; } = string.Empty;
        public OnlineApplicantLookUpResponseDTO? FollowupStatus { get; set; }

    }
}