
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
public record PaymentResponseDTO
{
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
}