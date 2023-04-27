
namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

public class Education
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? LevelofEducationId { get; set; }
    public LookUp? LevelofEducation { get; set; }
    public string? QualificationTypeId { get; set; }
    public LookUp? QualificationType { get; set; }
    public List<LookUp>? Awards { get; set; }
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
    
}
