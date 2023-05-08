
using AppDiv.SmartAgency.Application.Contracts.DTOs.CategoryDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record ApplicantsLookUpResponseDTO
{
    public Guid Id { get; set; }
    public string? Value { get; set; }
    public CategoryResponseDTO? Category { get; set; }
}