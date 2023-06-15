

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record GetApplForAssignmentDTO
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public int Age { get; set; }
    public string? PassportNumber { get; set; }
    public string? Religion { get; set; }
    public string? Language { get; set; }
}