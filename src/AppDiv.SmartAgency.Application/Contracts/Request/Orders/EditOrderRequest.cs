

using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record EditOrderRequest
{
    public Guid Id { get; set; }
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

    public EditFileCollectionRequest? VisaFile { get; set; }
    public EditOrderCriteriaRequest? OrderCriteria { get; set; }
    public EditPaymentRequest? OrderPayment { get; set; }
    public EditSponsorRequest? OrderSponsor { get; set; }
}