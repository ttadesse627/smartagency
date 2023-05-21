

namespace AppDiv.SmartAgency.Application.Contracts.Request.Common;
public record EditAttachmentFileRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? FilePath { get; set; }
    public Guid? AttachmentId { get; set; }
}