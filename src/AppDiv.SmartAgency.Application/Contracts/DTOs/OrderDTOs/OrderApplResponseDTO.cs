using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
public record OrderApplResponseDTO
{
    public Guid Id { get; set; }
    public string? PassportNumber { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public int? Age { get; set; }
    public LookUpResponseDTO? ApplicantReligion { get; set; }
    // public LanguageResponseDTO? ApplicantLanguage { get; set; }
}