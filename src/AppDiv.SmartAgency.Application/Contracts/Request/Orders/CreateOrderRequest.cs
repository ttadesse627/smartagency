

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

    // Foreign Keys
    public Guid? PortOfArrivalId { get; set; }
    public Guid? PriorityId { get; set; }
    public Guid? VisaTypeId { get; set; }
    public ICollection<Guid>? EmployeeIds { get; set; }
    public Guid? PartnerId { get; set; }

    // Navigation properties

    public AttachmentFileRequest? AttachmentFile { get; set; }
    public OrderCriteriaRequest? OrderCriteria { get; set; }
    public PaymentRequest? Payment { get; set; }
    public SponsorRequest? Sponsor { get; set; }
}