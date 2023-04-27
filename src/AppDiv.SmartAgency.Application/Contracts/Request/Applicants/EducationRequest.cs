using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class EducationRequest
{
    public string? LevelofEducationId { get; set; }
    public string? QualificationTypeId { get; set; }
    public List<string>? AwardId { get; set; }
    public int YearCompleted { get; set; }
    public string? FieldOfStudy { get; set; }
    public string? ProfessionalSkill { get; set; }
}