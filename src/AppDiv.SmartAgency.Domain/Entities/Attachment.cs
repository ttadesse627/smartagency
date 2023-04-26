using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Enums;
namespace AppDiv.SmartAgency.Domain.Entities;
public class Attachment : BaseAuditableEntity
{
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public AttachmentCategory Category { get; set; } = AttachmentCategory.ApplicantAttachment;
    public bool IsRequired { get; set; }
    public bool ShowOnCv { get; set; }
}