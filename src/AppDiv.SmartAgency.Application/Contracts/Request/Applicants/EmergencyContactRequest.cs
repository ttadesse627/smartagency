

using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class EmergencyContactRequest
{
    public string? ArabicFullName { get; set; }
    public Guid? EmergencyContactRelationshipId { get; set; }
    public AddressRequest? EmergencyContactAddress { get; set; }
}