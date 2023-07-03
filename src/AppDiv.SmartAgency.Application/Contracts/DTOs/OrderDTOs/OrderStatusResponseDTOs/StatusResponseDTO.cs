

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusResponseDTOs;
public record StatusResponseDTO
{
    public string? StatusName { get; set; }
    public DateTime? Date { get; set; }
}