

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record GetUnAssignedApplicantsDTO
{
    public ICollection<GetApplForAssignmentDTO>? UnAssignedApplicants { get; set; }
}