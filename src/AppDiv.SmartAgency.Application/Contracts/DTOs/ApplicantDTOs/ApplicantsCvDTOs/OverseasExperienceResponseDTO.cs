
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
public record OverseasExperienceResponseDTO
{
    public string? Country { get; set; }
    public int Period { get; set; }
    public string? Position { get; set; }
    
}