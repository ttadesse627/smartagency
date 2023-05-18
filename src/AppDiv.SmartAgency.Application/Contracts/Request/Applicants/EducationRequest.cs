
namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public record EducationRequest
{
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
    public ICollection<Guid>? QualificationTypes { get; set; }
    public ICollection<Guid>? LevelofQualifications{ get; set; }
    public ICollection<Guid>? Awards { get; set; }
}