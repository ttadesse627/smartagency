

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class WitnessRequest
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}