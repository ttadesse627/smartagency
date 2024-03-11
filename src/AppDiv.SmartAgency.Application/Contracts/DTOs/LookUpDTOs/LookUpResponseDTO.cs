
using AppDiv.SmartAgency.Application.Contracts.DTOs.CategoryDTOs;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs
{
    public record LookUpResponseDTO
    {
        public Guid Id { get; set; }
        public string? Category { get; set; }
        public string? Value { get; set; }
    }
}