
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;

public record EducationResponseDTO
{
    public ICollection<ApplicantsLookUpResponseDTO>? EducationLevelofQualifications{ get; set; }
    public ICollection<ApplicantsLookUpResponseDTO>? EducationQualificationTypes { get; set; }
    public ICollection<ApplicantsLookUpResponseDTO>? EducationAwards { get; set; }
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
    
}
