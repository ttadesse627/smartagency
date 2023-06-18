

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record ProcessStatusResponseDTO
{
    public string? StatusName { get; set; }
    public DateTime? Date { get; set; }
}