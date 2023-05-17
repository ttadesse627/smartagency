

namespace AppDiv.SmartAgency.Application.Contracts.Request.Common;
public record FileCollectionRequest
{
    public string? FilePath { get; set; }
    public Guid? FileCollectionAttachmentId { get; set; }
}