
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
public record LanguagesResponseDTO
{
    public string? LanguageName { get; set; }
    public string? Proficiency { get; set; }
    
}