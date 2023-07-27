

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public record SendApplicantRequest
{
    public Guid ApplicantId { get; set; }
    public Guid PartnerId { get; set; }
}