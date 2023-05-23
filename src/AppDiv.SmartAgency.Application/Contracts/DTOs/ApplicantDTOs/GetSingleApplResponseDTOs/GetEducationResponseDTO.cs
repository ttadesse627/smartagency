
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs. GetSingleApplResponseDTOs;
public record GetEducationResponseDTO
{
    public Guid Id { get; set; }
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
    public ICollection<GetQualificationTypeResponseDTO>? QualificationTypes { get; set; }
    public ICollection<GetLevelOfQualificationResponseDTO>? LevelofQualifications{ get; set; }
    public ICollection<GetAwardResponseDTO>? Awards { get; set; }
}