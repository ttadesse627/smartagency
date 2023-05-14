

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Orders;
public class Order : BaseAuditableEntity
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
    public Guid? PortOfArrivalId { get; set; }
    public Guid? PriorityId { get; set; }
    public Guid? VisaTypeId { get; set; }
    public Guid? EmployeeId { get; set; }
    public Guid? PartnerId { get; set; }
    public Guid? OrderCriteriaId { get; set; }
    public Guid? OrderPaymentId { get; set; }
    public Guid? OrderSponsorId { get; set; }

    // Navigation properties
    public Partner? Partner { get; set; }
    public LookUp? PortOfArrival { get; set; }
    public LookUp? Priority { get; set; }
    public LookUp? VisaType { get; set; }
    public Applicant? Employee { get; set; }
    public FileCollection? VisaFile { get; set; }
    public OrderCriteria? OrderCriteria { get; set; }
    public Payment? OrderPayment { get; set; }
    public Sponsor? OrderSponsor { get; set; }

}