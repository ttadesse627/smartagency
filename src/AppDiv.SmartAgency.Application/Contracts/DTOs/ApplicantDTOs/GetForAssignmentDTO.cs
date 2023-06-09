

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record GetApplForAssignmentDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public string PassportNumber { get; set; } = string.Empty;
    public DateTime IssuedDate { get; set; }
    public LookUpResponseDTO? Religion { get; set; }
    public LookUpResponseDTO? Jobtitle { get; set; }
    public LookUpResponseDTO? Language { get; set; }
    public LookUpResponseDTO? Salary { get; set; }
}