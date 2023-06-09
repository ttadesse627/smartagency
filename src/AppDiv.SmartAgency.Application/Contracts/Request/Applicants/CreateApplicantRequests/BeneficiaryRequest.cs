

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
public record BeneficiaryRequest
{
    public string? FullName { get; set; }
    public string? Region { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public float Rate { get; set; }
    public Guid? RelationshipId { get; set; }

}