using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class EducationRequest
{
    public ICollection<LevelOfQualificationLookUpRequest>? EducationLevelofQualifications { get; set; }
    public ICollection<QualificationTypeLookUpRequest>? EducationQualificationTypes { get; set; }
    public ICollection<AwardLookUpRequest>? EducationAawards { get; set; }
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
}