using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Domain.Enums;
namespace AppDiv.SmartAgency.Domain.Entities;
public class Attachment : BaseAuditableEntity
{
    public string? Title { get; set; }
    public AttachmentType Type { get; set; }
    public bool Required { get; set; }
    public bool ShowOnCv { get; set; }
    public ICollection<Applicant>? Applicants { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Sponsor>? Sponsors { get; set; }
}