using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class EducationRequest
{
    public ICollection<LevelOfQualificationRequest>? EducationLevelofQualifications{ get; set; }
    public ICollection<QualificationTypeRequst>? EducationQualificationTypes { get; set; }
    public ICollection<AwardRequest>? EducationAawards { get; set; }
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
}