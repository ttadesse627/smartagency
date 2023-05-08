using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record FileCollectionResponseDTO
{
    public Guid Id { get; set; }
    public string? FilePath { get; set; }
    public AttachmentResponseDTO? FileCollectionAttachment { get; set; }
}