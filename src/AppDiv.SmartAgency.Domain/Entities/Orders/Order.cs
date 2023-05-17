

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
    public Guid? OrderPortOfArrivalId { get; set; }
    public Guid? OrderPriorityId { get; set; }
    public Guid? OrderVisaTypeId { get; set; }
    public Guid? OrderEmployeeId { get; set; }
    public Guid? OrderPartnerId { get; set; }

    // Navigation properties
    public Partner? OrderPartner { get; set; }
    public LookUp? OrderPortOfArrival { get; set; }
    public LookUp? OrderPriority { get; set; }
    public LookUp? OrderVisaType { get; set; }
    public Applicant? OrderEmployee { get; set; }
    public FileCollection? OrderVisaFile { get; set; }
    public OrderCriteria? OrderCriteria { get; set; }
    public Payment? OrderPayment { get; set; }
    public Sponsor? OrderSponsor { get; set; }

}