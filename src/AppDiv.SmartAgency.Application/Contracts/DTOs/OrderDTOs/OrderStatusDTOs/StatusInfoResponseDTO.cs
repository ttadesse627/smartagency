

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record StatusInfoResponseDTO
{
    public ICollection<ProcessStatusResponseDTO>? ProcessStatuses { get; set; }
}