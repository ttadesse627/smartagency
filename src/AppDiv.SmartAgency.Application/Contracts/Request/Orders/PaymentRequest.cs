

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record PaymentRequest
{
    public int TotalAmount { get; set; }
    public int CurrentPaidAmount { get; set; }
}