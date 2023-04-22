

using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Attachments;
public record CreateAttachmentRequest 
{
    public string Code { get; set; }
    public string Description { get; set; }
    public AttachmentCategory Category { get; set; }
    public bool IsRequired { get; set; }
    public bool ShowOnCv { get; set; }
}