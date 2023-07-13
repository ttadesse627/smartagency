

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record EnjazResponseDTO
{
    public Guid Id {get; set;}
    public string? ApplicationNumber { get; set; }
    public string? TransactionCode { get; set; }
}