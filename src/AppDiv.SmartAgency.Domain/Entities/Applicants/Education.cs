
namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

public class Education
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public ICollection<LookUp>? LevelofEducationLookUps { get; set; }
    public string? QualificationTypeLookUpId { get; set; }
    public LookUp? QualificationTypeLookUp { get; set; }
    public string? AwardLookUpId { get; set; }
    public LookUp? AwardLookUp { get; set; }
    public ICollection<Applicant>? EducationApplicants { get; set; }
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
}
