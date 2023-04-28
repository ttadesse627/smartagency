

using AppDiv.SmartAgency.Domain.Entities.Applicants;

namespace AppDiv.SmartAgency.Domain.Entities.Base;
public class AttachmentFile
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string AttachmentId { get; set; }
    public Attachment? Attachment { get; set; }
    public string? FilePath { get; set; }
    public string ApplicantId { get; set; }
    public Applicant? ApplicantAttachmentFile { get; set; }
}