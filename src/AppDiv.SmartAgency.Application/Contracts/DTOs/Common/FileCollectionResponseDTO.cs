
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
public record AttachmentFileResponseDTO
{
    public string? AttachmentFile { get; set; }
    public AttachmentDropDownDto? Attachment { get; set; }
}