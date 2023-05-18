

namespace AppDiv.SmartAgency.Application.Contracts.Request.Common;
public record AttachmentFileRequest
{
    public string? FilePath { get; set; }
    public Guid? AttachmentId { get; set; }
}