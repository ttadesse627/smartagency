

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
    public Guid? PartnerId { get; set; }

    // Navigation properties

    public LookUp? PortOfArrival { get; set; }
    public LookUp? Priority { get; set; }
    public LookUp? VisaType { get; set; }
    public ICollection<Applicant>? Employees { get; set; }
    public AttachmentFile? AttachmentFile { get; set; }
    public OrderCriteria? OrderCriteria { get; set; }
    public Payment? Payment { get; set; }
    public Sponsor? Sponsor { get; set; }
    public Enjaz? Enjaz { get; set; }
    public Partner? Partner { get; set; }
    public ICollection<Complaint>? Complaints { get; set; }

}