using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Enums;
namespace AppDiv.SmartAgency.Domain.Entities;
public class Attachment : BaseAuditableEntity
{
    public string? Title { get; set; }
    public AttachmentType Type { get; set; }
    public bool Required { get; set; }
    public bool ShowOnCv { get; set; }

    // Navigation properties
    public ICollection<AttachmentFile>? AttachmentFiles { get; set; }
}