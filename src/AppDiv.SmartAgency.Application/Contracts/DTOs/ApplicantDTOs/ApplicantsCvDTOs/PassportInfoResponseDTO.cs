
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
public record PassportInfoResponseDTO
{
    public string? PassportNumber { get; set; }
    public string? IssuedDate { get; set; }
    public string? ExpiryDate { get; set; }
    public string? PassportIssuedPlace { get; set; }

    public string? NextOfKinName { get; set; }
    public string? NextOfKinNumber { get; set; }
    
}