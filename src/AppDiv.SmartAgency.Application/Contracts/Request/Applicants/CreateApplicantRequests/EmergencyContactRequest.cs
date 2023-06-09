

using AppDiv.SmartAgency.Application.Contracts.Request.Common;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
public record EmergencyContactRequest
{
    public string? NameOfContactPerson { get; set; }
    public string? ArabicName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public Guid? RelationshipId { get; set; }
    public AddressRequest? Address { get; set; }
}