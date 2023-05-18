

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Domain.Entities.Base;
public class AttachmentFile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? FilePath { get; set; }

    // Foreign Keys
    public Guid? AttachmentId { get; set; }
    public Guid? ApplicantId { get; set; }
    public Guid? OrderId { get; set; }

    // Navigation properties
    public Attachment? Attachment { get; set; }
    public Applicant? Applicant { get; set; }
    public Order? Order { get; set; }
    public Sponsor? Sponsor { get; set; }
}