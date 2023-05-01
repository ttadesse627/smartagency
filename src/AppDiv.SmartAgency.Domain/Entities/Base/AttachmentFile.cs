

using AppDiv.SmartAgency.Domain.Entities.Applicants;

namespace AppDiv.SmartAgency.Domain.Entities.Base;
public class AttachmentFile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AttachmentId { get; set; }
    public Attachment? Attachment { get; set; }
    public string? FilePath { get; set; }
    public Guid ApplicantId { get; set; }
    public Applicant? ApplicantAttachmentFile { get; set; }
}