

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests
{
    public record ApplicantExperienceRequest
    {
public ICollection<ExperienceRequest> Experiences { get; set; }
    }
}