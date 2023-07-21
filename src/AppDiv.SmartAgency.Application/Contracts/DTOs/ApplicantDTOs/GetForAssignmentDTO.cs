

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record GetApplForAssignmentDTO
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public int Age { get; set; }
    public string? PassportNumber { get; set; }
    public LookUpItemResponseDTO? Religion { get; set; }
    public LookUpItemResponseDTO? Language { get; set; }
    public LookUpItemResponseDTO? Nationality { get; set; }
    public LookUpItemResponseDTO? JobTitle { get; set; }
    public LookUpItemResponseDTO? Salary { get; set; }
    public LookUpItemResponseDTO? Experience { get; set; }
}