
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
public record AttachmentResponseDTO
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public AttachmentCategory Type { get; set; }
    public bool Required { get; set; }
    public bool ShowOnCv { get; set; }
}