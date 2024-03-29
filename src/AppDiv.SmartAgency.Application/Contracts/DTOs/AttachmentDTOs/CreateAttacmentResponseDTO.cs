using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
public record CreateAttachmentResponseDTO
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public AttachmentType Category { get; set; }
    public bool IsRequired { get; set; }
    public bool ShowOnCv { get; set; }
}