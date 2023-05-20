using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
public record AttachmentFileResponseDTO
{
    public Guid Id { get; set; }
    public string? FilePath { get; set; }
    public AttachmentResponseDTO? Attachment { get; set; }
}