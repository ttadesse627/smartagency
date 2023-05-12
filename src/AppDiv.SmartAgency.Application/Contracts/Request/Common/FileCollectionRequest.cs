

namespace AppDiv.SmartAgency.Application.Contracts.Request.Common;
public record FileCollectionRequest
{
    public Guid FileCollectionAttachmentId { get; set; }
    public string? FilePath { get; set; }
}