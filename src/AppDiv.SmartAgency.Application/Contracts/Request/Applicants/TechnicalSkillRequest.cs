
namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public record TechnicalSkillRequest
{
    public ICollection<string>? TechnicalSkillId { get; set; }
}