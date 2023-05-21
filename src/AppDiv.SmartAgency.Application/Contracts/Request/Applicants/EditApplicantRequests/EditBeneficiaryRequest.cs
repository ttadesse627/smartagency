

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants. EditApplicantRequests;
public record EditBeneficiaryRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Region { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public float Rate { get; set; }
    public Guid? RelationshipId { get; set; }

}