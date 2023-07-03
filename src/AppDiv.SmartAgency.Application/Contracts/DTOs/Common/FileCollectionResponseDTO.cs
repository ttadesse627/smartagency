using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
public record AttachmentFileResponseDTO
{
    public string? AttachmentFile { get; set; }
    public string? AttachmentType { get; set; }
}