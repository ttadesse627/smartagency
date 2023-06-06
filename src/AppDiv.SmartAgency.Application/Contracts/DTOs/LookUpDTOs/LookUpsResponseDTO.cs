

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs
{
    public record LookUpsResponseDTO
    {
        public string Category { get; set; }
        public List<LookUpItemResponseDTO> Items { get; set; }
    }
}