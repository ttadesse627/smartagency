

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
public class WitnessRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}