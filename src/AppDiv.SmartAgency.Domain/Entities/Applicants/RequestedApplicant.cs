
namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class RequestedApplicant
{
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid PartnerId { get; set; }
    public Applicant Applicant { get; set; } = null!;
    public Partner Partner { get; set; } = null!;
}