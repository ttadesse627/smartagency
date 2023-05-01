
namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

public class Education
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ICollection<LevelOfQualification>? EducationLevelofQualifications{ get; set; }
    public ICollection<QualificationType>? EducationQualificationTypes { get; set; }
    public ICollection<Award>? EducationAawards { get; set; }
    public Guid EducationApplicantId { get; set; }
    public Applicant? EducationApplicant { get; set; }
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
}
