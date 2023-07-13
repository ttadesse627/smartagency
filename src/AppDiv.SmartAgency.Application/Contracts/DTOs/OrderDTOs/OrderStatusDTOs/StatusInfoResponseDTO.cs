

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record StatusInfoResponseDTO
{
     public Guid Id {get; set;}
    public string? StatusName { get; set; }
    public DateTime? Date { get; set; }
   
}