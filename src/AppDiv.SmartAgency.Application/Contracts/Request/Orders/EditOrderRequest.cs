

using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record EditOrderRequest
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string VisaNumber { get; set; } = string.Empty;
    public int ContractDuration { get; set; }
    public DateTime? VisaDate { get; set; }
    public DateTime OrderDate { get; set; }
    public string? ContractNumber { get; set; }
    public string? ElectronicVisaNumber { get; set; }
    public DateTime? ElectronicVisaDate { get; set; }
    public Guid? PortOfArrivalId { get; set; }
    public Guid? PriorityId { get; set; }
    public Guid? VisaTypeId { get; set; }
    public ICollection<Guid>? EmployeeIds { get; set; }
    public Guid? PartnerId { get; set; }
    public EditAttachmentFileRequest? AttachmentFile { get; set; }
    public EditOrderCriteriaRequest? OrderCriteria { get; set; }
    public EditPaymentRequest? Payment { get; set; }
    public EditSponsorRequest? Sponsor { get; set; }
}