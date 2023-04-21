
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
public record CreateAttachmentDTO
{
    public string Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public bool IsRequired { get; set; }
    public bool ShowOnCv { get; set; }
}