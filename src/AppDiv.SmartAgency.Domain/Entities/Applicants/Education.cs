
namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

public class Education
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
    // Foreign Keys
    public Guid? EducationApplicantId { get; set; }
    
    // Navigation properties
    public Applicant? EducationApplicant { get; set; }
    public ICollection<LookUp>? EducationLevelofQualifications{ get; set; }
    public ICollection<LookUp>? EducationQualificationTypes { get; set; }
    public ICollection<LookUp>? EducationAwards { get; set; }
    
}
