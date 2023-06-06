

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
public class SkillRequest
{
    public ICollection<LanguageSkillRequest>? LanguageSkills { get; set; }
    public ICollection<Guid>? Skills { get; set; }
}