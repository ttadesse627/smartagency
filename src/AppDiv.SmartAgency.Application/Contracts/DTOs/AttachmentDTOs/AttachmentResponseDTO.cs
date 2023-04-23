
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
public record AttachmentResponseDTO
{
    public string Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public AttachmentCategory Category { get; set; }
    public bool IsRequired { get; set; }
    public bool ShowOnCv { get; set; }
}