

namespace AppDiv.SmartAgency.Application.Contracts.Request.Common;
public record EditAttachmentFileRequest
{
    public Guid Id { get; set; }
    public string? FilePath { get; set; }
    public Guid? AttachmentId { get; set; }
}