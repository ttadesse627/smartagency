

using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class EmergencyContactRequest
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Relationship { get; set; }
    public AddressRequest Address { get; set; }
}