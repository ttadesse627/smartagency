

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
public record WitnesRequest
{
    public RepresentativeRequest? Representative { get; set; }
    public ICollection<WitnessRequest>? Witnesses { get; set; }
}