

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Domain.Entities.Base;
public class FileCollection
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? FilePath { get; set; }

    // Foreign Keys
    public Guid? FileCollectionAttachmentId { get; set; }
    public Guid? FileCollectionApplicantId { get; set; }
    public Guid? FileCollectionOrderId { get; set; }

    // Navigation properties
    public Attachment? FileCollectionAttachment { get; set; }
    public Applicant? FileCollectionApplicant { get; set; }
    public Order? FileCollectionOrder { get; set; }
    public Sponsor? FileCollectionSponsor { get; set; }
}