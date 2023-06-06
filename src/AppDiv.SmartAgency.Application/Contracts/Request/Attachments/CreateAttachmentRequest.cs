

using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Attachments;
public record CreateAttachmentRequest
{
    public string? Title { get; set; }
    public AttachmentType Type { get; set; }
    public bool Required { get; set; }
    public bool ShowOnCv { get; set; }
}