

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record PaymentRequest
{
    public Guid Id { get; set; }
    public int TotalAmount { get; set; }
    public int PaidAmount { get; set; }
    public int CurrentPaidAmount { get; set; }
}