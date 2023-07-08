
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
public record AttachmentsResponseDTO
{
    public string? Photo { get; set; }
    public string? FullSizePhoto { get; set; }
    
}