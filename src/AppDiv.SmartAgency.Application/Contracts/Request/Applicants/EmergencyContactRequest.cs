

using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class EmergencyContactRequest
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string? ArabicFullName { get; set; }
    public Guid EmergencyContactRelationshipId { get; set; }
    public Guid EmergencyContactRegionId { get; set; }
    public AddressRequest? EmergencyContactAddress { get; set; }
}