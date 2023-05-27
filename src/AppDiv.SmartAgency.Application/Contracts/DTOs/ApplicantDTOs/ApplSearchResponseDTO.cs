
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record ApplSearchResponseDTO
{
    public int RefNumber { get; set; }
    public string? PassportNumber { get; set; }
    public int Age { get; set; }
    public LookUpResponseDTO? Religion { get; set; }
    public LookUpResponseDTO? Experience { get; set; }
    public LookUpResponseDTO? Jobtitle { get; set; }
    public LookUpResponseDTO? Language { get; set; }
}