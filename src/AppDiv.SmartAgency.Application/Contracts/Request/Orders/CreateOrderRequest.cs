

using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record CreateOrderRequest
{
    public string OrderNumber { get; set; }
    public string VisaNumber { get; set; }
    public int ContractDuration { get; set; }
    public DateTime? VisaDate { get; set; }
    public string? ContractNumber { get; set; }
    public string? ElectronicVisaNumber { get; set; }
    public DateTime? ElectronicVisaDate { get; set; }
    public Guid? PortOfArrivalId { get; set; }
    public Guid? PriorityId { get; set; }
    public Guid? VisaTypeId { get; set; }
    public FileCollectionRequest? VisaFile { get; set; }
    public OrderCriteriaRequest? OrderCriteria { get; set; }
    public PaymentRequest? OrderPayment { get; set; }
    public SponsorRequest? OrderSponsor { get; set; }
}