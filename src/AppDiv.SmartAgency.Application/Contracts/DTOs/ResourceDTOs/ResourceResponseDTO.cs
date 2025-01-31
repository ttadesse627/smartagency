namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ResourceDTOs
{
    public record ResourceResponseDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}