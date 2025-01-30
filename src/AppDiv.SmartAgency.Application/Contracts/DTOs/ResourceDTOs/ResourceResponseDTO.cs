

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ResourceDTOs
{
    public record ResourceResponseDTO
    {
        public Guid Id { get; set; }
        public string? Category { get; set; }
        public string? Value { get; set; }
    }
}