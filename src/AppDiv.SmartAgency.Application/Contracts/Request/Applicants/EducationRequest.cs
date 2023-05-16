using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class EducationRequest
{
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
    public ICollection<Guid>? EducationLevelofQualifications{ get; set; }
    public ICollection<Guid>? EducationQualificationTypes { get; set; }
    public ICollection<Guid>? EducationAwards { get; set; }
}