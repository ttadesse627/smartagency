using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
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
    public LookUpResponseDTO? EmergencyContactRelationship { get; set; }
    public LookUpResponseDTO? EmergencyContactRegion { get; set; }
    public LookUpResponseDTO? EmergencyContactApplicant { get; set; }
    public AddressResponseDTO? EmergencyContactAddress { get; set; }
}