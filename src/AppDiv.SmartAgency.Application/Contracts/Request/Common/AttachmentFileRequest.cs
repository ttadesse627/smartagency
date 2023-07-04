

namespace AppDiv.SmartAgency.Application.Contracts.Request.Common;
public record AttachmentFileRequest
{
    public string AttachmentFile { get; set; }
    public Guid AttachmentId { get; set; }
}