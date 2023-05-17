

using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record CreateOrderRequest
{
    public string OrderNumber { get; set; } = string.Empty;
    public string VisaNumber { get; set; } = string.Empty;
    public int ContractDuration { get; set; }
    public DateTime? VisaDate { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public string? ContractNumber { get; set; }
    public string? ElectronicVisaNumber { get; set; }
    public DateTime? ElectronicVisaDate { get; set; }
    public bool IsDeleted { get; set; } = false;

    // Foreign Keys
    public Guid? OrderPortOfArrivalId { get; set; }
    public Guid? OrderPriorityId { get; set; }
    public Guid? OrderVisaTypeId { get; set; }
    public Guid? OrderEmployeeId { get; set; }
    public Guid? OrderPartnerId { get; set; }

    // Navigation properties
    public OrderCriteriaRequest? OrderCriteria { get; set; }
    public PaymentRequest? OrderPayment { get; set; }
    public SponsorRequest? OrderSponsor { get; set; }
}