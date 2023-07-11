
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record ApplSearchResponseDTO
{
    public Guid? Id{get; set;}
    public string? FullName {get; set;}
    public int RefNumber { get; set; }
    public string? PassportNumber { get; set; }
    public int Age { get; set; }
    public LookUpResponseDTO? Religion { get; set; }
    public LookUpResponseDTO? Experience { get; set; }
    public LookUpResponseDTO? Jobtitle { get; set; }
    public LookUpResponseDTO? Language { get; set; }
    public string? Path {get; set;}

    public string? Photo {get; set;}
}