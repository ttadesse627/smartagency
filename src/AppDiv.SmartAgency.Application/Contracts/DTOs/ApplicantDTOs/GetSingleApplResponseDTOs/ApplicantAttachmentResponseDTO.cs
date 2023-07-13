

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
public record ApplicantAttachmentResponseDTO
{
    public string AttachmentFile { get; set; }
    public string AttachmentType { get; set; }
}