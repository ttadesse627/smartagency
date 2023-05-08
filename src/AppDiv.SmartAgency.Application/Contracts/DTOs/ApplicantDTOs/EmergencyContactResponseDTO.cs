using AppDiv.SmartAgency.Application.Contracts.DTOs.AddressDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record EmergencyContactResponseDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? ArabicFullName { get; set; }
    public ApplicantsLookUpResponseDTO? EmergencyContactRelationship { get; set; }
    public ApplicantsLookUpResponseDTO? EmergencyContactRegion { get; set; }
    public ApplicantsLookUpResponseDTO? EmergencyContactApplicant { get; set; }
    public AddressResponseDTO? EmergencyContactAddress { get; set; }
}