
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;

public record EducationResponseDTO
{
    public ICollection<LookUpResponseDTO>? EducationLevelofQualifications{ get; set; }
    public ICollection<LookUpResponseDTO>? EducationQualificationTypes { get; set; }
    public ICollection<LookUpResponseDTO>? EducationAwards { get; set; }
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
    
}
