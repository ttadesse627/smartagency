

using AppDiv.SmartAgency.Domain.Entities.Applicants;

namespace AppDiv.SmartAgency.Domain.Entities.Base;
public class FileCollection
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? FilePath { get; set; }

    // Foreign Keys
    public Guid FileCollectionAttachmentId { get; set; }
    public Guid FileCollectionApplicantId { get; set; }

    // Navigation properties
    public Attachment? FileCollectionAttachment { get; set; }
    public Applicant? FileCollectionApplicant { get; set; }
}