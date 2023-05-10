
using AppDiv.SmartAgency.Application.Contracts.DTOs.CategoryDTOs;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs
{
    public class LookUpResponseDTO
    {
    public Guid Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public CategoryResponseDTO Category { get; set; }
    }
}