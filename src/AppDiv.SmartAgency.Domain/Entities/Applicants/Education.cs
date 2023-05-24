
namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

public class Education
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
    // Foreign Keys
    public Guid? ApplicantId { get; set; }
    
    // Navigation properties
    public Applicant? Applicant { get; set; }
    public ICollection<QualificationType>? QualificationTypes { get; set; }
    public ICollection<LevelOfQualification>? LevelOfQualifications{ get; set; }
    public ICollection<Award>? Awards { get; set; }
    
}
