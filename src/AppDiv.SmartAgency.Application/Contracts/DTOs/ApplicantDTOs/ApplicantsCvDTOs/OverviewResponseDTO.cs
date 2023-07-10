
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
public record OverviewResponseDTO
{
    public string? RefNumber { get; set; }
    public string? FullName { get; set; }
    public string? Religion { get; set; }
    public string? DesiredPosition { get; set; }
    public string? Salary { get; set; }
    public int Age { get; set; }
    public string? Sex { get; set; }
    
}