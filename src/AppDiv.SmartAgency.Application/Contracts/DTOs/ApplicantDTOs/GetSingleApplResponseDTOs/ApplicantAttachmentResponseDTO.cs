

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
public record ApplicantAttachmentResponseDTO
{
    public required string AttachmentFile { get; set; }
    public required string AttachmentType { get; set; }
}