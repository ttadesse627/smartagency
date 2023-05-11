

namespace AppDiv.SmartAgency.Application.Contracts.Request.Common;
public record EditFileCollectionRequest
{
    public Guid Id { get; set; }
    public Guid FileCollectionAttachmentId { get; set; }
    public string? FilePath { get; set; }
}