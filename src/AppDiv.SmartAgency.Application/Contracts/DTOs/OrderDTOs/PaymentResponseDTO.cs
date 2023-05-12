
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
public record PaymentResponseDTO
{
    public int TotalAmount { get; set; }
    public int PaidAmount { get; set; }
    public int CurrentPaidAmount { get; set; }
}