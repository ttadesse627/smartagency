
namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record EditPaymentRequest
{
    public Guid Id { get; set; }
    public int TotalAmount { get; set; }
    public int CurrentPaidAmount { get; set; }
}