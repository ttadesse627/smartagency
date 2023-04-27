

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class AttachmentFile
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    // public string? AttachmentId { get; set; }
    // public Attachment? Attachment { get; set; }
    public string? FilePath { get; set; }
    // public Applicant? Applicant { get; set; }
}