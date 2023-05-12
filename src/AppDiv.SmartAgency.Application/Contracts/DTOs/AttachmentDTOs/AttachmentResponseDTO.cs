
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
public record AttachmentResponseDTO
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public AttachmentCategory Category { get; set; }
    public bool IsRequired { get; set; }
    public bool ShowOnCv { get; set; }
}