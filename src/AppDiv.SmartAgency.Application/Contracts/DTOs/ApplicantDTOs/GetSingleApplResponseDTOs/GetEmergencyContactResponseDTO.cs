

using AppDiv.SmartAgency.Application.Contracts.Request.Common;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs. GetSingleApplResponseDTOs;
public record GetEmergencyContactResponseDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public string? ArabicFullName { get; set; }
    public Guid? RelationshipId { get; set; }
    public EditAddressRequest? Address { get; set; }
}